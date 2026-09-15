using EntityStates;
using ProjectSynth.Character.Synth.Content;

namespace ProjectSynth.States
{
    public class CultureShockState : ShockState
    {
        public override void OnEnter()
        {
            overlayMaterial = SynthAssets.mat_cultureShockOverlayMain;
            stunVfxPrefab = SynthAssets.vfx_cultureShock;

            shockDuration = SynthValues.CultureShockDuration;
            enterSoundString = AssignRandomSoundString();
            exitSoundString = "";
            healthFractionToForceExit = 0.1f;

            base.OnEnter();
        }

        private string AssignRandomSoundString()
        {
            return Sounds.CultureShock[UnityEngine.Random.Range(0, Sounds.CultureShock.Length)];
        }
    }
}
