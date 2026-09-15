using UnityEngine;
using UnityEngine.Networking;

namespace ProjectSynth.States.Synth.Diva
{
    public class WaitForStick : BaseDivaState
    {
        private ParticleSystem particleSystem;

        public override void OnEnter()
        {
            base.OnEnter();
            particleSystem = transform.Find("DivaVisuals/Trail/Particles").GetComponent<ParticleSystem>();

            if (NetworkServer.active)
            {
                ArmingStateMachine.SetState(new DivaArmingUnarmed());
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (NetworkServer.active)
            {
                var psm = particleSystem.main;
                psm.loop = !IsStuck;
                if (IsStuck)
                {
                    outer.SetNextState(new Arm());
                }
            }
        }
    }
}
