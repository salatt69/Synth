using ProjectSynth.Character.Synth.Content;
using UnityEngine.Networking;

namespace ProjectSynth.States.Synth.Diva
{
    public class Arm : BaseDivaState
    {
        public float duration = SynthValues.DivaArmingDuration;

        public override void OnEnter()
        {
            base.OnEnter();

            //TODO: play animation
            //PlayAnimation("", "", "", duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (NetworkServer.active && fixedAge >= duration)
            {
                outer.SetNextState(new StunningPerformance());
            }
        }
    }
}
