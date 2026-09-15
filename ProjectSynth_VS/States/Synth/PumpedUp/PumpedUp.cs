using ProjectSynth.Character.Synth.Content;
using ProjectSynth.Components;
using ProjectSynth.Mod;
using ProjectSynth.Modules.BaseContent.BaseStates.Metro;
using ProjectSynth.States.Synth.Metro;
using RoR2;
using SYNClib.API;
using UnityEngine;

namespace ProjectSynth.States.Synth.PumpedUp
{
    public class PumpedUp : BaseMetroSkillState
    {
        public int durationInBeats = SynthValues.PumpedUpDurationInBeats;
        public float fovBonus = SynthValues.PumpedUpFOVBonus;
        public float lerpDuration = SynthValues.PumpedUpFOVLerpDuration;
        public Material overlayEffectMaterial = SynthAssets.mat_PumpedUpOverlayEffectMaterial;

        private CharacterCameraOverride cameraOverride;
        private CharacterCameraOverride.Modifier fovModifier;
        private CharacterCameraOverride.ModifierHandle fovHandle;

        private CharacterShaderOverlay shaderOverlay;

        private bool wasMetronomeHit;
        private int beatCount;

        public override void OnEnter()
        {
            base.OnEnter();
            characterBody.AddBuff(SynthBuffs.PumpedUp);

            cameraOverride = gameObject.GetComponent<CharacterCameraOverride>();
            if (cameraOverride == null)
            {
                Log.Warning("Camera override component not found on character.");
            }
            else
            {
                fovModifier = (ref CameraState state, float alpha) => state.fov += fovBonus * alpha;
                fovHandle = cameraOverride.AddModifier(fovModifier, lerpDuration, CharacterCameraOverride.EaseOutQuint);
            }

            shaderOverlay = characterBody.GetComponent<CharacterShaderOverlay>();
            if (shaderOverlay == null)
            {
                Log.Warning("Shader overlay component not found on character.");
            }
            else
            {
                shaderOverlay.effectMaterial = overlayEffectMaterial;
            }

            shaderOverlay.SetActive(true, lerpDuration / 2f);
        }

        public override void Update()
        {
            base.Update();
            if (Sync.OnBeat()) beatCount++;

            if (beatCount >= durationInBeats && !wasMetronomeHit)
            {
                outer.SetNextStateToMain();
            }
            else if (wasMetronomeHit)
            {
                if (Input.GetKeyDown(KeyCode.F3))
                {
                    outer.SetNextStateToMain();
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (characterBody.HasBuff(SynthBuffs.PumpedUp))
            {
                characterBody.RemoveBuff(SynthBuffs.PumpedUp);
            }

            cameraOverride?.RemoveModifier(fovHandle, lerpDuration, CharacterCameraOverride.EaseInOut);
            shaderOverlay?.SetActive(false, lerpDuration / 2f);
        }

        public override void OnMetronomeHit(BaseMetroState metroState)
        {
            base.OnMetronomeHit(metroState);

            wasMetronomeHit = true;
        }

        public override void OnMetronomeMiss(BaseMetroState metroState)
        {
            base.OnMetronomeMiss(metroState);

            wasMetronomeHit = false;
        }
    }
}
