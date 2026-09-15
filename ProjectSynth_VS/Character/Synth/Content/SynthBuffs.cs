using R2API;
using RoR2;
using UnityEngine;

namespace ProjectSynth.Character.Synth.Content
{
    public static class SynthBuffs
    {
        public static BuffDef Encore { get; private set; }
        public static BuffDef WeakEnd { get; private set; }
        public static BuffDef PumpedUp { get; private set; }

        public static void Init()
        {
            Encore = ScriptableObject.CreateInstance<BuffDef>();
            Encore.name = "Encore";
            Encore.buffColor = Color.magenta;
            Encore.canStack = true;
            Encore.isDebuff = true;
            Encore.eliteDef = null;
            Encore.iconSprite = SynthAssets.tex_icon_EncoreBuff;
            ContentAddition.AddBuffDef(Encore);

            WeakEnd = ScriptableObject.CreateInstance<BuffDef>();
            WeakEnd.name = "WeakEnd";
            WeakEnd.buffColor = Color.red;
            WeakEnd.canStack = false;
            WeakEnd.isDebuff = true;
            WeakEnd.eliteDef = null;
            WeakEnd.iconSprite = SynthAssets.tex_icon_WeakEndBuff;
            ContentAddition.AddBuffDef(WeakEnd);

            PumpedUp = ScriptableObject.CreateInstance<BuffDef>();
            PumpedUp.name = "PumpedUp";
            PumpedUp.buffColor = Color.yellow;
            PumpedUp.canStack = false;
            PumpedUp.isDebuff = false;
            PumpedUp.eliteDef = null;
            PumpedUp.iconSprite = SynthAssets.tex_icon_PumpedUpBuff;
            ContentAddition.AddBuffDef(PumpedUp);
        }
    }
}
