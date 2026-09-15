using ProjectSynth.Character.Synth.Content;
using RoR2;
using SYNClib.API;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class EncoreRuntime
{
    private static readonly GameObject encoreExplosionPrefab = SynthAssets.vfx_encoreExplosion;

    private class Sequence
    {
        public CharacterBody victim;
        public CharacterBody attacker;
    }

    private static readonly List<Sequence> active = [];

    public static void TryStartSequence(CharacterBody victim, CharacterBody attacker)
    {
        if (!NetworkServer.active) return;

        bool exists = active.Any(s => s.victim == victim && s.attacker == attacker);
        if (exists) return;

        active.Add(new Sequence
        {
            victim = victim,
            attacker = attacker,
        });
    }

    public static void Process()
    {
        if (!NetworkServer.active) return;
        if (active.Count == 0) return;

        for (int i = active.Count - 1; i >= 0; i--)
        {
            var s = active[i];

            if (!s.victim || !s.attacker)
            {
                active.RemoveAt(i);
                continue;
            }

            if (Sync.OnBeat())
            {
                Fire(s);

                int buffCount = s.victim.GetBuffCount(SynthBuffs.Encore.buffIndex);
                s.victim.SetBuffCount(SynthBuffs.Encore.buffIndex, buffCount - 1);
                int newBuffCount = s.victim.GetBuffCount(SynthBuffs.Encore.buffIndex);

                if (newBuffCount <= 0)
                {
                    active.RemoveAt(i);
                }
            }
        }
    }

    private static void Fire(Sequence sequence)
    {
        float victimMass;
        if (sequence.victim.characterMotor != null)
        {
            victimMass = sequence.victim.characterMotor.mass;
        }
        else if (sequence.victim.rigidbody != null)
        {
            victimMass = sequence.victim.rigidbody.mass;
        }
        else
        {
            victimMass = 0f;
        }

        float explosionRadius = 3f + (victimMass * 0.0035f); // very precise 0.35% radius increase per unit of mass.
        new BlastAttack
        {
            radius = explosionRadius,
            baseDamage = sequence.attacker.damage * SynthValues.EncoreDamageScaleCoefficient,
            damageType = DamageType.AOE,
            falloffModel = BlastAttack.FalloffModel.None,
            attacker = sequence.attacker.gameObject,
            inflictor = sequence.attacker.gameObject,
            teamIndex = sequence.attacker.teamComponent.teamIndex,
            position = sequence.victim.corePosition,
            crit = sequence.attacker.RollCrit()
        }.Fire();
        if (encoreExplosionPrefab)
        {
            EffectManager.SpawnEffect(encoreExplosionPrefab, new EffectData
            {
                origin = sequence.victim.corePosition,
                scale = explosionRadius
            }, false);
        }
    }
}
