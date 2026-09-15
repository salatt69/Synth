using UnityEngine;

namespace ProjectSynth.Modules
{
    public static class Components
    {
        public static T AddOrGet<T>(GameObject go) where T : Component
        {
            return go.GetComponent<T>() ?? go.AddComponent<T>();
        }

        internal static void DestroyChild(this GameObject root, string name)
        {
            var t = root.transform.Find(name);
            if (t)
                UnityEngine.Object.DestroyImmediate(t.gameObject);
        }
    }
}
