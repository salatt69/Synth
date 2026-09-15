using EntityStates;
using ProjectSynth.Components;

namespace ProjectSynth.States.Synth.Metro
{
    public abstract class BaseMetroState : BaseState
    {
        protected SynthMetroRuntime metro;
        public bool IsOnCooldown { get; protected set; }

        public override void OnEnter()
        {
            base.OnEnter();

            metro = gameObject.GetComponent<SynthMetroRuntime>();
        }

        public bool IsInTimingWindow => metro != null && metro.timingWindowOpen;

        // idk if grading will be used for anything
        public MetroGrade Grade => metro != null ? metro.grade : MetroGrade.None;

        public void EnterCooldownState()
        {
            outer.SetNextState(new MetroCooldownState());
        }

        public void EnterMissedState()
        {
            outer.SetNextState(new MetroMissedState());
        }
    }
}