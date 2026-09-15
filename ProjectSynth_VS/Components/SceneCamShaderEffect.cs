using UnityEngine;

namespace ProjectSynth.Components
{
    // i hate it
    public class SceneCamShaderEffect : MonoBehaviour
    {
        public Material effectMaterial;
        [Range(0f, 1f)] public float intensity;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (effectMaterial == null || intensity <= 0f)
            {
                Graphics.Blit(source, destination);
                return;
            }

            effectMaterial.SetFloat("_Intensity", intensity);
            Graphics.Blit(source, destination, effectMaterial);
        }
    }
}