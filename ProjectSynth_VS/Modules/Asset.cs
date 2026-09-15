using ProjectSynth.Mod;
using System.Collections.Generic;
using UnityEngine;
using Path = System.IO.Path;

namespace ProjectSynth.Modules
{
    internal static class Asset
    {
        internal static Dictionary<string, AssetBundle> loadedBundles = [];

        internal static AssetBundle LoadAssetBundle(string bundleName)
        {
            if (loadedBundles.ContainsKey(bundleName))
            {
                return loadedBundles[bundleName];
            }

            AssetBundle assetBundle = null;
            try
            {
                assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(SynthPlugin.instance.Info.Location), "AssetBundles", bundleName));
            }
            catch (System.Exception e)
            {
                Log.Error($"Error loading asset bundle, {bundleName}. Your asset bundle must be in a folder next to your mod dll called 'AssetBundles'. Follow the guide to build and install your mod correctly!\n{e}");
            }

            loadedBundles[bundleName] = assetBundle;

            return assetBundle;

        }

        internal static void ConvertAllRenderersToHopooShader(GameObject objectToConvert)
        {
            if (!objectToConvert) return;

            foreach (MeshRenderer i in objectToConvert.GetComponentsInChildren<MeshRenderer>())
            {
                if (i)
                {
                    if (i.sharedMaterial)
                    {
                        i.sharedMaterial.ConvertStubbedShaderToHopoo_Standart();
                    }
                }
            }

            foreach (SkinnedMeshRenderer i in objectToConvert.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (i)
                {
                    if (i.sharedMaterial)
                    {
                        i.sharedMaterial.ConvertStubbedShaderToHopoo_Standart();
                    }
                }
            }
        }
    }
}