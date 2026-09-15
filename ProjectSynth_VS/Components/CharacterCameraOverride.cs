using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSynth.Components
{
    public class CharacterCameraOverride : MonoBehaviour, ICameraStateProvider
    {
        public delegate void Modifier(ref CameraState state, float alpha);

        public readonly struct ModifierHandle
        {
            internal readonly ModifierEntry entry;
            internal ModifierHandle(ModifierEntry entry) { this.entry = entry; }
            public bool IsValid => entry != null;
        }

        internal class ModifierEntry
        {
            public Modifier modifier;
            public AnimationCurve curve;
            public float enterStartTime;
            public float enterEndTime;
            public float exitStartTime = float.PositiveInfinity;
            public float exitEndTime = float.PositiveInfinity;

            public float CalculateAlpha(float time)
            {
                float alpha = 1f;
                if (time < enterEndTime)
                    alpha *= Mathf.Clamp01(Mathf.InverseLerp(enterStartTime, enterEndTime, time));
                if (time > exitStartTime)
                    alpha *= Mathf.Clamp01(Mathf.InverseLerp(exitEndTime, exitStartTime, time));
                return curve.Evaluate(alpha);
            }

            public bool IsFinished(float time) => time >= exitEndTime;
        }

        public static readonly AnimationCurve EaseOutQuint = new(
            new Keyframe(0f, 0.000000f, 5.000000f, 5.000000f),
            new Keyframe(0.25f, 0.762695f, 1.582031f, 1.582031f),
            new Keyframe(0.5f, 0.968750f, 0.312500f, 0.312500f),
            new Keyframe(0.75f, 0.999023f, 0.019531f, 0.019531f),
            new Keyframe(1f, 1.000000f, 0.000000f, 0.000000f)
        );
        public static readonly AnimationCurve EaseInOut = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        public static readonly AnimationCurve Linear = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private static readonly AnimationCurve defaultCurve = EaseInOut;

        private readonly List<ModifierEntry> entries = [];
        private CameraRigController cameraRigController;

        public ModifierHandle AddModifier(Modifier modifier, float inDuration = 0.2f, AnimationCurve curve = null)
        {
            float time = Time.time;
            var entry = new ModifierEntry
            {
                modifier = modifier,
                curve = curve ?? defaultCurve,
                enterStartTime = time,
                enterEndTime = time + inDuration
            };

            bool wasEmpty = entries.Count == 0;
            entries.Add(entry);
            if (wasEmpty) EnsureOverrideInstalled();

            return new ModifierHandle(entry);
        }

        public void RemoveModifier(ModifierHandle handle, float outDuration = 0.2f, AnimationCurve curve = null)
        {
            if (!handle.IsValid) return;
            float time = Time.time;
            handle.entry.curve = curve ?? defaultCurve;
            handle.entry.exitStartTime = time;
            handle.entry.exitEndTime = time + outDuration;
        }

        private void Update()
        {
            float time = Time.time;
            for (int i = entries.Count - 1; i >= 0; i--)
            {
                if (entries[i].IsFinished(time))
                    entries.RemoveAt(i);
            }
            if (entries.Count == 0)
                RemoveOverride();
        }

        private void EnsureOverrideInstalled()
        {
            if (cameraRigController == null)
            {
                var body = GetComponent<CharacterBody>();
                foreach (var crc in CameraRigController.readOnlyInstancesList)
                {
                    if (crc.targetBody == body)
                    {
                        cameraRigController = crc;
                        break;
                    }
                }
            }
            cameraRigController?.SetOverrideCam(this, 0f);
        }

        private void RemoveOverride()
        {
            if (cameraRigController != null && cameraRigController.IsOverrideCam(this))
                cameraRigController.SetOverrideCam(null, 0f);
        }

        public void GetCameraState(CameraRigController crc, ref CameraState state)
        {
            float time = Time.time;
            for (int i = 0; i < entries.Count; i++)
            {
                float alpha = entries[i].CalculateAlpha(time);
                entries[i].modifier(ref state, alpha);
            }
        }

        public bool IsUserLookAllowed(CameraRigController crc) => true;
        public bool IsUserControlAllowed(CameraRigController crc) => true;
        public bool IsHudAllowed(CameraRigController crc) => true;
    }
}
