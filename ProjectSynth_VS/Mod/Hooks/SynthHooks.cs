using ProjectSynth.Character.Synth.Content;
using ProjectSynth.States;
using R2API;
using RoR2;
using SYNClib.API;
using UnityEngine;

namespace ProjectSynth.Mod.Hooks
{
    internal class SynthHooks
    {
        public void Initialize()
        {
            //On.RoR2.Run.FixedUpdate += On_RoR2_Run_FixedUpdate;
            On.RoR2.Run.Update += On_RoR2_Run_Update;
            On.RoR2.HealthComponent.TakeDamage += On_RoR2_HealthComponent_TakeDamage;
            RecalculateStatsAPI.GetStatCoefficients += RecalculateStatsAPI_GetStatCoefficients;
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void On_RoR2_Run_FixedUpdate(On.RoR2.Run.orig_FixedUpdate orig, Run self)
        {
            orig(self);
        }

        private void On_RoR2_Run_Update(On.RoR2.Run.orig_Update orig, Run self)
        {
            orig(self);

            EncoreRuntime.Process();
        }

        private void On_RoR2_HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (damageInfo.damage > 0)
            {
                if (self.body.HasBuff(SynthBuffs.WeakEnd))
                {
                    //int count = self.body.GetBuffCount(SynthBuffs.WeakEnd.buffIndex);
                    damageInfo.damage *= SynthValues.WeakEndDamageMultiplier;
                    damageInfo.procCoefficient = 1f;
                }
            }

            orig(self, damageInfo);
        }

        private void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, R2API.RecalculateStatsAPI.StatHookEventArgs args)
        {
            if (sender.HasBuff(SynthBuffs.PumpedUp))
            {
                int bpm = (int)Sync.BPM;

                float bpmAddition = bpm * 0.01f * SynthValues.PumpedUpBPMMultiplierScaling;
                float pumpedUpMult = SynthValues.PumpedUpMultiplierBase + bpmAddition;

                args.moveSpeedTotalMult *= pumpedUpMult;
                args.attackSpeedTotalMult *= pumpedUpMult;
                args.damageTotalMult *= pumpedUpMult;
                args.healthTotalMult *= pumpedUpMult;
                args.regenTotalMult *= pumpedUpMult;

                args.armorTotalMult *= pumpedUpMult;
                args.shieldTotalMult *= pumpedUpMult;

                args.critDamageTotalMult *= pumpedUpMult;
                args.critTotalMult *= pumpedUpMult;

                args.jumpPowerTotalMult *= pumpedUpMult;

                args.luckAdd += Mathf.Pow(pumpedUpMult, SynthValues.PumpedUpLuckPower);
            }
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            if (report == null) return;

            if (report.damageInfo.HasModdedDamageType(SynthDamageTypes.Encore))
            {
                CharacterBody victim = report.victimBody;
                CharacterBody attacker = report.attackerBody;

                int count = victim.GetBuffCount(SynthBuffs.Encore.buffIndex);
                victim.SetBuffCount(SynthBuffs.Encore.buffIndex, count + SynthValues.EncoreInflictedStacksAmount);
                EncoreRuntime.TryStartSequence(victim, attacker);
            }
            if (report.damageInfo.HasModdedDamageType(SynthDamageTypes.CultureShock))
            {
                CharacterBody victim = report.victimBody;

                victim?.GetComponent<SetStateOnHurt>()?.SetCustomState(
                    EntityStateCatalog.GetStateIndex(typeof(CultureShockState)),
                    EntityStates.InterruptPriority.Stun
                    );
            }
            if (report.damageInfo.HasModdedDamageType(SynthDamageTypes.WeakEnd))
            {
                CharacterBody victim = report.victimBody;
                float duration = SynthValues.WeakEndDebuffDuration;
                victim.AddTimedBuff(SynthBuffs.WeakEnd, duration);
                victim.SetTimedBuffDurationIfPresent(SynthBuffs.WeakEnd, duration, true);
            }
        }
    }
}
