using EntityStates;
using ProjectSynth.Character.Synth.Content;
using ProjectSynth.Modules.BaseContent.BaseStates.Metro;
using ProjectSynth.States.Synth.Metro;
using R2API;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace ProjectSynth.States.Synth.Weapon
{
    public class TNM : BaseMetroSkillState
    {
        public GameObject projectilePrefab = SynthAssets.proj_ThirtyNineMusic;
        public GameObject muzzleFlashPrefab = SynthAssets.vfx_tnmMuzzleFlash;
        public float duration = SynthValues.ThirtyNineMusicDuration;
        public float damageCoefficient = SynthValues.ThirtyNineMusicDamageCoefficient;
        public float force = SynthValues.ThirtyNineMusicProjectileForce;
        public float bloom = SynthValues.ThirtyNineMusicBloom;
        public float recoilAmplitude = SynthValues.ThirtyNineMusicRecoilAmplitude;
        public string attackSoundString;
        public string attackSoundStringAlt;

        private bool hasFired;
        private Animator animator;
        private ChildLocator childLocator;
        private Transform muzzleTransform;
        private readonly string muzzleString = "Tie";
        private DamageTypeCombo damageCombo;

        public override void OnEnter()
        {
            base.OnEnter();

            characterBody.SetAimTimer(2f);
            animator = GetModelAnimator();
            if (animator)
            {
                childLocator = animator.GetComponent<ChildLocator>();
            }

            Fire();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void Fire()
        {
            if (hasFired) return;

            characterBody.AddSpreadBloom(bloom);
            Ray aimRay = GetAimRay();
            if (childLocator)
            {
                muzzleTransform = childLocator.FindChild(muzzleString);
            }
            if (muzzleFlashPrefab)
            {
                //EffectManager.SimpleMuzzleFlash(muzzleFlashPrefab, gameObject, muzzleString, false);
            }
            if (isAuthority)
            {
                float damage = damageStat * damageCoefficient;
                FireProjectileInfo fireProjectileInfo = new()
                {
                    projectilePrefab = projectilePrefab,
                    position = muzzleTransform.position + aimRay.direction * 2f, // slightly in front of the muzzle. for now
                    rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                    owner = this.gameObject,
                    target = null,
                    useSpeedOverride = false,
                    useFuseOverride = false,
                    damage = damage,
                    force = force,
                    crit = RollCrit(),
                    damageColorIndex = DamageColorIndex.Default,
                    damageTypeOverride = damageCombo
                };
                ProjectileManager.instance.FireProjectile(fireProjectileInfo);
            }
            AddRecoil(-0.1f * recoilAmplitude, 0.1f * recoilAmplitude, -1.0f * recoilAmplitude, 1.0f * recoilAmplitude);

            hasFired = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void OnMetronomeHit(BaseMetroState metroState)
        {
            base.OnMetronomeHit(metroState);

            damageCombo = new()
            {
                damageType = DamageType.Generic,
            };
            damageCombo.AddModdedDamageType(SynthDamageTypes.Encore);
        }

        public override void OnMetronomeMiss(BaseMetroState metroState)
        {
            base.OnMetronomeMiss(metroState);
        }
    }
}
