namespace ProjectSynth.Character.Synth.Content
{
    public static class SynthValues
    {
        // Diva numbers
        public static float DivaArmingDuration { get; set; } = 1.0f;
        public static float DivaExplosionDamageCoefficient { get; set; } = 6.0f;
        public static float DivaProjectileSpeed { get; set; } = 60.0f;

        // Miku Miku Beam numbers
        public static float MikuBeamLeapUpwardSpeed { get; set; } = 25.0f;
        public static float MikuBeamLeapForwardSpeed { get; set; } = 10.0f;
        public static int MikuBeamSustainDurationInBeats { get; set; } = 12;
        public static float MikuBeamLength { get; set; } = 135.0f;
        public static float MikuBeamDamageCoefficient { get; set; } = 16.0f;

        // Rolling Girl numbers
        public static int RollingGirlDurationInBeats { get; set; } = 5;
        public static float RollingGirlSpeedMultiplier { get; set; } = 1.5f;
        public static float RollingGirlEnemyGrabRadius { get; set; } = 10.0f;
        public static float RollingGirlEnemyGrabForceMagnitude { get; set; } = -4500.0f;
        public static float RollingGirlEnemyGrabForceCoefficientAtEdge { get; set; } = 0.5f;
        public static float RollingGirlEnemyGrabForceDamping { get; set; } = 0.5f;

        // 39 Music numbers
        public static float ThirtyNineMusicDuration { get; set; } = 0.9f;
        public static float ThirtyNineMusicDamageCoefficient { get; set; } = 1.6f;
        public static float ThirtyNineMusicProjectileForce { get; set; } = 20.0f;
        public static float ThirtyNineMusicBloom { get; set; } = 5.0f;
        public static float ThirtyNineMusicRecoilAmplitude { get; set; } = 1.8f;

        // Metro numbers
        public static int MetroSuccessfulHitCooldownInBeats { get; set; } = 4;

        // WeakEnd numbers
        public static float WeakEndDamageMultiplier { get; set; } = 1.25f;
        public static float WeakEndDebuffDuration { get; set; } = 3.0f;

        // PumpedUp numbers
        public static float PumpedUpBPMMultiplierScaling { get; set; } = 0.75f;
        public static float PumpedUpMultiplierBase { get; set; } = 1.0f;
        public static float PumpedUpLuckPower { get; set; } = 5.0f;
        public static int PumpedUpDurationInBeats { get; set; } = 5;
        public static float PumpedUpFOVBonus { get; set; } = 30.0f;
        public static float PumpedUpFOVLerpDuration { get; set; } = 1.0f;

        // Encore numbers
        public static int EncoreInflictedStacksAmount { get; set; } = 2;
        public static float EncoreDamageScaleCoefficient { get; set; } = 0.8f;

        // Culture Shock numbers
        public static float CultureShockDuration { get; set; } = 1.5f;
    }
}