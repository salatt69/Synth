using RoR2;
using SYNClib.API;
using UnityEngine;

namespace ProjectSynth.Components
{
    public enum MetroGrade
    {
        None,
        Good,
        Perfect
    }

    public sealed class SynthMetroRuntime : MonoBehaviour
    {
        public CharacterBody body;

        public float clock;

        public double lastBeatTime = -1.0;

        public float speedMult = 1f;

        public int beatIndex;

        public float beatPhase01;
        public bool timingWindowOpen;
        public MetroGrade grade;

        public float goodWindowHalfBeats = 0.10f;
        public float perfectWindowHalfBeats = 0.04f;

        // UI
        public bool ongoing;

        public bool cooldownStartedThisFrame;
        public float cooldownSpeedMult = 1f;

        public bool fumbledThisFrame;

        private void Awake()
        {
            if (speedMult <= 0f) speedMult = 1f;
        }

        private void Update()
        {
            clock += Time.deltaTime;

            if (Sync.OnEntry())
            {
                ResetAll();
                return;
            }

            if (Sync.OnBeat())
            {
                lastBeatTime = clock;
                beatIndex = (int)Sync.BeatIndex;
            }

            ComputePhaseAndWindow();
            ongoing = Sync.BeatInterval > 0.0;
        }

        private void ComputePhaseAndWindow()
        {
            grade = MetroGrade.None;
            timingWindowOpen = false;

            double interval = Sync.BeatInterval;

            if (interval <= 0.0 || lastBeatTime < 0.0)
            {
                beatPhase01 = 0f;
                return;
            }

            double t = clock - lastBeatTime;

            float phase = (float)(t / interval);
            phase -= Mathf.Floor(phase);
            beatPhase01 = phase;

            float dist = Mathf.Min(phase, 1f - phase);

            if (dist <= perfectWindowHalfBeats)
            {
                timingWindowOpen = true;
                grade = MetroGrade.Perfect;
            }
            else if (dist <= goodWindowHalfBeats)
            {
                timingWindowOpen = true;
                grade = MetroGrade.Good;
            }

            speedMult = (float)(1.0 / interval);
        }

        public void ResetAll()
        {
            clock = 0f;

            lastBeatTime = -1.0;

            speedMult = 1f;
            beatIndex = 0;

            beatPhase01 = 0f;
            timingWindowOpen = false;
            grade = MetroGrade.None;

            ongoing = false;

            cooldownStartedThisFrame = false;
            cooldownSpeedMult = 1f;
            fumbledThisFrame = false;
        }
    }
}
