using EntityStates;
using ProjectSynth.Modules;
using ProjectSynth.States.Synth;
using ProjectSynth.States.Synth.PumpedUp;
using ProjectSynth.States.Synth.Weapon;
using R2API;
using RoR2.Skills;

namespace ProjectSynth.Character.Synth.Content
{
    class SynthSkillDefs
    {
        private static string Prefix => SynthSurvivor.SYNTH_PREFIX;

        public static PassiveItemSkillDef Passive_Metro()
        {
            PassiveItemSkillDef metro = Skills.CreateSkillDef(new PassiveItemSkillDefInfo
            {
                skillName = "M1K-U",
                skillNameToken = Prefix + "PASSIVE_METRO_NAME",
                skillDescriptionToken = Prefix + "PASSIVE_METRO_DESCRIPTION",
                icon = SynthAssets.tex_icon_Metro,

                activationStateMachineName = "Body",
                activationState = new SerializableEntityStateType(typeof(SynthMain)),
                interruptPriority = InterruptPriority.Any,

                baseRechargeInterval = 0,
                baseMaxStock = 0,
                rechargeStock = 0,
                requiredStock = 0,
                stockToConsume = 0,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = false,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = false,
                mustKeyPress = false,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false,
                passiveItem = SynthPassive.MetroPassiveItem
            });
            ContentAddition.AddSkillDef(metro);
            return metro;
        }
        public static PassiveItemSkillDef Passive_Another()
        {
            PassiveItemSkillDef another = Skills.CreateSkillDef(new PassiveItemSkillDefInfo
            {
                skillName = "M1K-U v2.0",
                skillNameToken = Prefix + "PASSIVE_ANOTHER_NAME",
                skillDescriptionToken = Prefix + "PASSIVE_ANOTHER_DESCRIPTION",
                icon = SynthAssets.tex_icon_Metro,

                activationStateMachineName = "Weapon",
                activationState = new SerializableEntityStateType(typeof(SynthMain)),
                interruptPriority = InterruptPriority.Any,

                baseRechargeInterval = 0,
                baseMaxStock = 0,
                rechargeStock = 0,
                requiredStock = 0,
                stockToConsume = 0,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = false,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = false,
                mustKeyPress = false,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false,
                passiveItem = SynthPassive.AnotherPassiveItem
            });
            ContentAddition.AddSkillDef(another);
            return another;
        }
        public static SkillDef Primary_ThirtyNineMusic()
        {
            SkillDef tnm = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "39 Music!",
                skillNameToken = Prefix + "PRIMARY_THIRTY_NINE_MUSIC_NAME",
                skillDescriptionToken = Prefix + "PRIMARY_THIRTY_NINE_MUSIC_DESCRIPTION",
                keywordTokens = [Prefix + "KEYWORD_FOLLOW_THE_RHYTHM"],
                icon = SynthAssets.tex_icon_ThirtyNineMusic,

                activationStateMachineName = "Weapon",
                activationState = new SerializableEntityStateType(typeof(TNM)),
                interruptPriority = InterruptPriority.Any,

                baseRechargeInterval = 0,
                baseMaxStock = 0,
                rechargeStock = 0,
                requiredStock = 0,
                stockToConsume = 0,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = false,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = true,
                mustKeyPress = false,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false,
            });
            ContentAddition.AddSkillDef(tnm);
            return tnm;
        }
        public static SkillDef Secondary_DeployDiva()
        {
            SkillDef diva = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Virtual Deviation",
                skillNameToken = Prefix + "SECONDARY_VIRTUAL_DEVIATION_NAME",
                skillDescriptionToken = Prefix + "SECONDARY_VIRTUAL_DEVIATION_DESCRIPTION",
                icon = SynthAssets.tex_icon_Diva,

                activationStateMachineName = "Weapon",
                activationState = new SerializableEntityStateType(typeof(DeployDiva)),
                interruptPriority = InterruptPriority.Skill,

                baseRechargeInterval = 13f,
                baseMaxStock = 1,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = false,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = true,
                mustKeyPress = true,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false
            });
            ContentAddition.AddSkillDef(diva);
            return diva;
        }
        public static SkillDef Utility_RollingGirl()
        {
            SkillDef rollingGirl = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Rolling Girl",
                skillNameToken = Prefix + "UTILITY_ROLLING_GIRL_NAME",
                skillDescriptionToken = Prefix + "UTILITY_ROLLING_GIRL_DESCRIPTION",
                icon = SynthAssets.tex_icon_RollingGirl,

                activationStateMachineName = "Weapon",
                activationState = new SerializableEntityStateType(typeof(RollingGirl)),
                interruptPriority = InterruptPriority.Skill,

                baseRechargeInterval = 6f,
                baseMaxStock = 1,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = false,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = false,
                mustKeyPress = true,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false
            });
            ContentAddition.AddSkillDef(rollingGirl);
            return rollingGirl;
        }
        public static SkillDef Utility_PumpedUp()
        {
            SkillDef pumpedUp = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Pumped Up",
                skillNameToken = Prefix + "UTILITY_PUMPED_UP_NAME",
                skillDescriptionToken = Prefix + "UTILITY_PUMPED_UP_DESCRIPTION",
                keywordTokens = [Prefix + "KEYWORD_FOLLOW_THE_RHYTHM"],
                icon = SynthAssets.tex_icon_RollingGirl,

                activationStateMachineName = "PumpedUp",
                activationState = new SerializableEntityStateType(typeof(PumpedUp)),
                interruptPriority = InterruptPriority.Skill,

                baseRechargeInterval = 10f,
                baseMaxStock = 2,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = false,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = false,
                mustKeyPress = true,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false
            });
            ContentAddition.AddSkillDef(pumpedUp);
            return pumpedUp;
        }
        public static SkillDef Special_MikuBeam()
        {
            SkillDef mikuBeam = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Miku Miku Beam!",
                skillNameToken = Prefix + "SPECIAL_MIKU_BEAM_NAME",
                skillDescriptionToken = Prefix + "SPECIAL_MIKU_BEAM_DESCRIPTION",
                icon = SynthAssets.tex_icon_MikuBeam,

                activationStateMachineName = "Weapon",
                activationState = new SerializableEntityStateType(typeof(MikuBeamLeap)),
                interruptPriority = InterruptPriority.PrioritySkill,

                baseRechargeInterval = 4f,
                baseMaxStock = 1,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                attackSpeedBuffsRestockSpeed = false,
                attackSpeedBuffsRestockSpeed_Multiplier = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                beginSkillCooldownOnSkillEnd = true,
                isCooldownBlockedUntilManuallyReset = false,

                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
                canceledFromSprinting = false,

                isCombatSkill = false,
                mustKeyPress = true,
                triggeredByPressRelease = false,
                autoHandleLuminousShot = true,
                suppressSkillActivation = false,
                hideStockCount = false,
                hideCooldown = false
            });
            ContentAddition.AddSkillDef(mikuBeam);
            return mikuBeam;
        }
    }
}
