using ProjectSynth.Character.Synth.Content;
using R2API;
using RoR2;
using SYNClib.API;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjectSynth.States.Synth.Diva
{
    public class StunningPerformance : BaseDivaState
    {
        private readonly GameObject stunningPerformanceDistortionPrefab = SynthAssets.vfx_stunningPerformanceDistortion;
        private readonly GameObject stunningPerformanceShockPrefab = SynthAssets.vfx_stunningPerformanceShock;
        private float radius;

        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                ArmingStateMachine.SetState(new DivaArmingArmed());
            }

            var asm = ArmingStateMachine?.state as BaseDivaArmingState;
            radius = asm.ShockFieldRadius;
        }

        public override void Update()
        {
            base.Update();

            if (Sync.OnCustomBar())
            {
                Fire(SynthDamageTypes.CultureShock);
            }
            else if (Sync.OnBeat())
            {
                Fire(SynthDamageTypes.WeakEnd);
            }
        }

        private void Fire(DamageAPI.ModdedDamageType damageType)
        {
            GameObject spherePrefab = damageType == SynthDamageTypes.CultureShock ? stunningPerformanceDistortionPrefab : stunningPerformanceShockPrefab;

            DamageTypeCombo damageCombo = new()
            {
                damageType = DamageType.Generic
            };
            damageCombo.AddModdedDamageType(damageType);

            new BlastAttack
            {
                radius = radius,
                baseDamage = 0f,
                damageType = damageCombo,
                falloffModel = BlastAttack.FalloffModel.None,
                attacker = gameObject,
                teamIndex = TeamIndex.Player,
                position = base.transform.position
            }.Fire();
            if (spherePrefab)
            {
                EffectManager.SpawnEffect(spherePrefab, new EffectData
                {
                    origin = base.transform.position,
                    scale = radius
                }, false);
            }
        }
    }
}
