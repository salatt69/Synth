using HarmonyLib;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectSynth.Mod.Patches
{
    [HarmonyPatch(typeof(CrosshairUtils), nameof(CrosshairUtils.GetCrosshairPrefabForBody))]
    public static class PumpedUpSprintCrosshairFix
    {
        private static GameObject sprintingCrosshairPrefab;

        private static void Postfix(CharacterBody body, ref GameObject __result)
        {
            if (body == null || !body.isSprinting || !body.useSprintCrosshair) return;

            CameraRigController cameraRigController = null;
            foreach (var crc in CameraRigController.readOnlyInstancesList)
            {
                if (crc.targetBody == body) { cameraRigController = crc; break; }
            }

            if (cameraRigController == null || !cameraRigController.hasOverride) return;

            if (sprintingCrosshairPrefab == null)
                sprintingCrosshairPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/UI/SprintingCrosshair.prefab").WaitForCompletion();

            __result = body.sprintCrosshairPrefabOverride
                ? body.sprintCrosshairPrefabOverride
                : sprintingCrosshairPrefab;
        }
    }
}
