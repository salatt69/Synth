using BepInEx.Configuration;
using ProjectSynth.Modules;

namespace ProjectSynth.Character.Synth.Content
{
    // Currently used for testing purposes, but will be used for more configuration options in the future.
    public static class SynthConfig
    {
        #region values
        public static ConfigEntry<float> divaArmingDuration;
        public static ConfigEntry<float> divaExplosionDamageCoefficient;
        public static ConfigEntry<float> divaProjectileSpeed;

        public static ConfigEntry<float> mikuBeamLeapUpwardSpeed;
        public static ConfigEntry<float> mikuBeamLeapForwardSpeed;
        public static ConfigEntry<int> mikuBeamSustainDurationInBeats;
        public static ConfigEntry<float> mikuBeamLength;
        public static ConfigEntry<float> mikuBeamDamageCoefficient;

        public static ConfigEntry<int> rollingGirlDurationInBeats;
        public static ConfigEntry<float> rollingGirlSpeedMultiplier;
        public static ConfigEntry<float> rollingGirlEnemyGrabRadius;
        public static ConfigEntry<float> rollingGirlEnemyGrabForceMagnitude;
        public static ConfigEntry<float> rollingGirlEnemyGrabForceCoefficientAtEdge;
        public static ConfigEntry<float> rollingGirlEnemyGrabForceDamping;

        public static ConfigEntry<float> thirtyNineMusicDuration;
        public static ConfigEntry<float> thirtyNineMusicDamageCoefficient;
        public static ConfigEntry<float> thirtyNineMusicProjectileForce;
        public static ConfigEntry<float> thirtyNineMusicBloom;
        public static ConfigEntry<float> thirtyNineMusicRecoilAmplitude;

        public static ConfigEntry<int> metroSuccessfulHitCooldownInBeats;

        public static ConfigEntry<float> weakEndDamageMultiplier;
        public static ConfigEntry<float> weakEndDebuffDuration;

        public static ConfigEntry<float> pumpedUpBPMMultiplierScaling;
        public static ConfigEntry<float> pumpedUpMultiplierBase;
        public static ConfigEntry<float> pumpedUpLuckPower;
        public static ConfigEntry<int> pumpedUpDurationInBeats;
        public static ConfigEntry<float> pumpedUpFOVBonus;
        public static ConfigEntry<float> pumpedUpFOVLerpDuration;

        public static ConfigEntry<int> encoreInflictedStacksAmount;
        public static ConfigEntry<float> encoreDamageScaleCoefficient;

        public static ConfigEntry<float> cultureShockDuration;
        #endregion

        public static void Init()
        {
            ConfigForTesting();
        }

        private static void ConfigForTesting()
        {
            #region Diva Section
            string divaSection = "Diva";

            string divaArmingDurationDescription = "The amount of time (in seconds) it takes for the Diva to arm after its landing, before it starts functioning.";
            divaArmingDuration = Config.BindAndReasignPassedValueOnChange(divaSection, "Arming Duration", divaArmingDurationDescription, SynthValues.DivaArmingDuration, v => SynthValues.DivaArmingDuration = v);

            string divaExplosionDamageCoefficientDescription = "The damage coefficient of the Diva's explosion at the end of its lifetime.";
            divaExplosionDamageCoefficient = Config.BindAndReasignPassedValueOnChange(divaSection, "Explosion Damage Coefficient", divaExplosionDamageCoefficientDescription, SynthValues.DivaExplosionDamageCoefficient, v => SynthValues.DivaExplosionDamageCoefficient = v);

            string divaProjectileSpeedDescription = "The speed of the Diva projectile when thrown.";
            divaProjectileSpeed = Config.BindAndReasignPassedValueOnChange(divaSection, "Projectile Speed", divaProjectileSpeedDescription, SynthValues.DivaProjectileSpeed, v => SynthValues.DivaProjectileSpeed = v);
            #endregion

            #region Miku Beam Section
            string mikuBeamSection = "Miku Miku Beam";

            string mikuBeamLeapUpwardSpeedDescription = "The upward speed of the leap at the begining.";
            mikuBeamLeapUpwardSpeed = Config.BindAndReasignPassedValueOnChange(mikuBeamSection, "Leap Upward Speed", mikuBeamLeapUpwardSpeedDescription, SynthValues.MikuBeamLeapUpwardSpeed, v => SynthValues.MikuBeamLeapUpwardSpeed = v);

            string mikuBeamLeapForwardSpeedDescription = "The forward speed of the leap at the begining.";
            mikuBeamLeapForwardSpeed = Config.BindAndReasignPassedValueOnChange(mikuBeamSection, "Leap Forward Speed", mikuBeamLeapForwardSpeedDescription, SynthValues.MikuBeamLeapForwardSpeed, v => SynthValues.MikuBeamLeapForwardSpeed = v);

            string mikuBeamSustainDurationInBeatsDescription = "The duration of the beam firing in beats.";
            mikuBeamSustainDurationInBeats = Config.BindAndReasignPassedValueOnChange(mikuBeamSection, "Sustain Duration in Beats", mikuBeamSustainDurationInBeatsDescription, SynthValues.MikuBeamSustainDurationInBeats, v => SynthValues.MikuBeamSustainDurationInBeats = v);

            string mikuBeamLengthDescription = "The length of the beam.";
            mikuBeamLength = Config.BindAndReasignPassedValueOnChange(mikuBeamSection, "Beam Length", mikuBeamLengthDescription, SynthValues.MikuBeamLength, v => SynthValues.MikuBeamLength = v);

            string mikuBeamDamageCoefficientDescription = "The damage coefficient of the beam. This value gets multiplied by 'damageStat' to get the final damage value.";
            mikuBeamDamageCoefficient = Config.BindAndReasignPassedValueOnChange(mikuBeamSection, "Damage Coefficient", mikuBeamDamageCoefficientDescription, SynthValues.MikuBeamDamageCoefficient, v => SynthValues.MikuBeamDamageCoefficient = v);
            #endregion

            #region Rolling Girl Section
            string rollingGirlSection = "Rolling Girl";

            string rollingGirlDurationInBeatsDescription = "The duration of rolling (in beats).";
            rollingGirlDurationInBeats = Config.BindAndReasignPassedValueOnChange(rollingGirlSection, "Duration", rollingGirlDurationInBeatsDescription, SynthValues.RollingGirlDurationInBeats, v => SynthValues.RollingGirlDurationInBeats = v);

            string rollingGirlSpeedMultiplierDescription = "The multiplier of rolling speed. This value is multiplied by 'characterDirection.forward' and 'chracterBody.moveSpeed' to get final speed value.";
            rollingGirlSpeedMultiplier = Config.BindAndReasignPassedValueOnChange(rollingGirlSection, "Speed Multiplier", rollingGirlSpeedMultiplierDescription, SynthValues.RollingGirlSpeedMultiplier, v => SynthValues.RollingGirlSpeedMultiplier = v);

            string rollingGirlEnemyGrabRadiusDescription = "The radius of the enemy grab area.";
            rollingGirlEnemyGrabRadius = Config.BindAndReasignPassedValueOnChange(rollingGirlSection, "Enemy Grab Radius", rollingGirlEnemyGrabRadiusDescription, SynthValues.RollingGirlEnemyGrabRadius, v => SynthValues.RollingGirlEnemyGrabRadius = v);

            string rollingGirlEnemyGrabForceMagnitudeDescription = "The magnitude of the force applied when grabbing an enemy. (How hard it pulls an enemy)";
            rollingGirlEnemyGrabForceMagnitude = Config.BindAndReasignPassedValueOnChange(rollingGirlSection, "Enemy Grab Force Magnitude", rollingGirlEnemyGrabForceMagnitudeDescription, SynthValues.RollingGirlEnemyGrabForceMagnitude, v => SynthValues.RollingGirlEnemyGrabForceMagnitude = v);

            string rollingGirlEnemyGrabForceCoefficientAtEdgeDescription = "The force coefficient at the edge of the enemy grab area. (The higher the value, the more force is allowed to be applied to an enemy on the edges of grab raduis)";
            rollingGirlEnemyGrabForceCoefficientAtEdge = Config.BindAndReasignPassedValueOnChange(rollingGirlSection, "Enemy Grab Force Coefficient at Edge", rollingGirlEnemyGrabForceCoefficientAtEdgeDescription, SynthValues.RollingGirlEnemyGrabForceCoefficientAtEdge, v => SynthValues.RollingGirlEnemyGrabForceCoefficientAtEdge = v);

            string rollingGirlEnemyGrabForceDampingDescription = "The damping factor for the force applied when grabbing an enemy. (Smoothness of grab)";
            rollingGirlEnemyGrabForceDamping = Config.BindAndReasignPassedValueOnChange(rollingGirlSection, "Enemy Grab Force Damping", rollingGirlEnemyGrabForceDampingDescription, SynthValues.RollingGirlEnemyGrabForceDamping, v => SynthValues.RollingGirlEnemyGrabForceDamping = v);
            #endregion

            #region Thirty Nine Music Section
            string thirtyNineMusicSection = "Thirty Nine Music";

            string thirtyNineMusicDurationDescription = "The time between shots (in seconds).";
            thirtyNineMusicDuration = Config.BindAndReasignPassedValueOnChange(thirtyNineMusicSection, "Fire Rate", thirtyNineMusicDurationDescription, SynthValues.ThirtyNineMusicDuration, v => SynthValues.ThirtyNineMusicDuration = v);

            string thirtyNineMusicDamageCoefficientDescription = "The damage coefficient for the projectiles. This value is multiplied by 'damageStat' to get the final damage value";
            thirtyNineMusicDamageCoefficient = Config.BindAndReasignPassedValueOnChange(thirtyNineMusicSection, "Damage Coefficient", thirtyNineMusicDamageCoefficientDescription, SynthValues.ThirtyNineMusicDamageCoefficient, v => SynthValues.ThirtyNineMusicDamageCoefficient = v);

            string thirtyNineMusicProjectileForceDescription = "The force of the projectiles. (How hard it pushes enemies back)";
            thirtyNineMusicProjectileForce = Config.BindAndReasignPassedValueOnChange(thirtyNineMusicSection, "Projectile Force", thirtyNineMusicProjectileForceDescription, SynthValues.ThirtyNineMusicProjectileForce, v => SynthValues.ThirtyNineMusicProjectileForce = v);

            string thirtyNineMusicBloomDescription = "The bloom effect intensity for the. (The bigger the value is, the wider crosshair gets)";
            thirtyNineMusicBloom = Config.BindAndReasignPassedValueOnChange(thirtyNineMusicSection, "Bloom", thirtyNineMusicBloomDescription, SynthValues.ThirtyNineMusicBloom, v => SynthValues.ThirtyNineMusicBloom = v);

            string thirtyNineMusicRecoilAmplitudeDescription = "The amplitude of the recoil effect.";
            thirtyNineMusicRecoilAmplitude = Config.BindAndReasignPassedValueOnChange(thirtyNineMusicSection, "Recoil Amplitude", thirtyNineMusicRecoilAmplitudeDescription, SynthValues.ThirtyNineMusicRecoilAmplitude, v => SynthValues.ThirtyNineMusicRecoilAmplitude = v);
            #endregion

            #region Metro Section
            string metroSection = "Metronome (M1K-U)";

            string metroSuccessfulHitCooldownInBeatsDescription = "The amount of beats it takes for metronome to be able to receive input again.";
            metroSuccessfulHitCooldownInBeats = Config.BindAndReasignPassedValueOnChange(metroSection, "Successful Hit Cooldown", metroSuccessfulHitCooldownInBeatsDescription, SynthValues.MetroSuccessfulHitCooldownInBeats, v => SynthValues.MetroSuccessfulHitCooldownInBeats = v);
            #endregion

            #region WeakEnd Section
            string weakEndSection = "WeakEnd";

            string weakEndDamageMultiplierDescription = "The multiplier of damage taken on the next hit, by an entity with this debuff.";
            weakEndDamageMultiplier = Config.BindAndReasignPassedValueOnChange(weakEndSection, "Damage Multiplier", weakEndDamageMultiplierDescription, SynthValues.WeakEndDamageMultiplier, v => SynthValues.WeakEndDamageMultiplier = v);

            string weakEndDebuffDurationDescription = "The amount of time debuff stays on an entity after it has been applied.";
            weakEndDebuffDuration = Config.BindAndReasignPassedValueOnChange(weakEndSection, "Debuff Duration", weakEndDebuffDurationDescription, SynthValues.WeakEndDebuffDuration, v => SynthValues.WeakEndDebuffDuration = v);
            #endregion

            #region Pumped Up Section
            string pumpedUpSection = "Pumped Up";

            string pumpedUpBPMMultiplierScalingDescription = "Scales how much bonus BPM is added, as a percentage of current BPM, while the Pumped Up buff is active. BPM bonus is caluculated like this: 'bpmAddition = BPM * 0.01 * THIS_VALUE'";
            pumpedUpBPMMultiplierScaling = Config.BindAndReasignPassedValueOnChange(pumpedUpSection, "BPM Multiplier Scaling", pumpedUpBPMMultiplierScalingDescription, SynthValues.PumpedUpBPMMultiplierScaling, v => SynthValues.PumpedUpBPMMultiplierScaling = v);

            string pumpedUpMultiplierBaseDescription = "BPM multiplier's base. This is the vale to which 'bpmAddition' is being added. Increasing this value makes adds raw, unaffected boost to Pumped Up Buff.";
            pumpedUpMultiplierBase = Config.BindAndReasignPassedValueOnChange(pumpedUpSection, "Multiplier Base", pumpedUpMultiplierBaseDescription, SynthValues.PumpedUpMultiplierBase, v => SynthValues.PumpedUpMultiplierBase = v);

            string pumpedUpLuckPowerDescription = "Unlike every other stat on a list, luck is increased via addition, not multiplication. So to compensate for that, luck is added by a power of the calculated number. This number is exactly that power. (luck = pumpedUpMult^THIS_NUMBER)";
            pumpedUpLuckPower = Config.BindAndReasignPassedValueOnChange(pumpedUpSection, "Luck Power", pumpedUpLuckPowerDescription, SynthValues.PumpedUpLuckPower, v => SynthValues.PumpedUpLuckPower = v);

            string pumpedUpDurationInBeatsDescription = "The amount of time buff effect lasts (in beats)";
            pumpedUpDurationInBeats = Config.BindAndReasignPassedValueOnChange(pumpedUpSection, "Duration", pumpedUpDurationInBeatsDescription, SynthValues.PumpedUpDurationInBeats, v => SynthValues.PumpedUpDurationInBeats = v);

            string pumpedUpFOVBonusDescription = "The amount of FOV bonus applied when the Pumped Up buff is active.";
            pumpedUpFOVBonus = Config.BindAndReasignPassedValueOnChange(pumpedUpSection, "FOV Bonus", pumpedUpFOVBonusDescription, SynthValues.PumpedUpFOVBonus, v => SynthValues.PumpedUpFOVBonus = v);

            string pumpedUpFOVLerpDurationDescription = "The amount of time it takes to go from base FOV to base FOV + bonus FOV.";
            pumpedUpFOVLerpDuration = Config.BindAndReasignPassedValueOnChange(pumpedUpSection, "FOV Lerp Duration", pumpedUpFOVLerpDurationDescription, SynthValues.PumpedUpFOVLerpDuration, v => SynthValues.PumpedUpFOVLerpDuration = v);
            #endregion

            #region Encore Section
            string encoreSection = "Encore";

            string encoreInflictedStacksAmountDescription = "The amount of stacks inflicted at once.";
            encoreInflictedStacksAmount = Config.BindAndReasignPassedValueOnChange(encoreSection, "Inflicted Stacks Amount", encoreInflictedStacksAmountDescription, SynthValues.EncoreInflictedStacksAmount, v => SynthValues.EncoreInflictedStacksAmount = v);

            string encoreDamageScaleCoefficientDescription = "";
            encoreDamageScaleCoefficient = Config.BindAndReasignPassedValueOnChange(encoreSection, "Damage Scale Coefficient", encoreDamageScaleCoefficientDescription, SynthValues.EncoreDamageScaleCoefficient, v => SynthValues.EncoreDamageScaleCoefficient = v);
            #endregion

            #region CultureShock Section
            string cultureShockSection = "CultureShock";

            string cultureShockDurationDescription = "The amount of time entity with this effect will stay stunned.";
            cultureShockDuration = Config.BindAndReasignPassedValueOnChange(cultureShockSection, "Duration", cultureShockDurationDescription, SynthValues.CultureShockDuration, v => SynthValues.CultureShockDuration = v);
            #endregion
        }
    }
}
