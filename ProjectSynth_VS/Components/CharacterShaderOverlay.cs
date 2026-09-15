using RoR2;
using UnityEngine;

namespace ProjectSynth.Components
{
    public class CharacterShaderOverlay : MonoBehaviour
    {
        public Material effectMaterial;

        private CameraRigController cameraRigController;
        private SceneCamShaderEffect sceneEffect;

        private float targetIntensity;
        private float currentIntensity;
        private float blendDuration = 0.5f;
        private float blendVelocity;

        private void ResolveTargets()
        {
            if (cameraRigController == null)
            {
                var body = GetComponent<CharacterBody>();
                foreach (var crc in CameraRigController.readOnlyInstancesList)
                {
                    if (crc.targetBody == body) { cameraRigController = crc; break; }
                }
            }

            if (cameraRigController != null && sceneEffect == null && cameraRigController.sceneCam != null)
            {
                sceneEffect = cameraRigController.sceneCam.GetComponent<SceneCamShaderEffect>();
                if (sceneEffect == null)
                    sceneEffect = cameraRigController.sceneCam.gameObject.AddComponent<SceneCamShaderEffect>();

                sceneEffect.effectMaterial = effectMaterial;
            }
        }

        public void SetActive(bool active, float duration = 0.2f)
        {
            ResolveTargets();
            targetIntensity = active ? 1f : 0f;
            blendDuration = Mathf.Max(duration, 0.0001f);
        }

        private void Update()
        {
            if (sceneEffect == null) return;

            currentIntensity = Mathf.SmoothDamp(currentIntensity, targetIntensity, ref blendVelocity, blendDuration);
            sceneEffect.intensity = currentIntensity;
        }
    }
}