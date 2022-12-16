using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public static class PrefabEditorUtils {

        public static void CreatePrefab(DBEntry dBEntry, string key) {
            if (dBEntry == null) {
                return;
            }
            if (string.IsNullOrEmpty(key)) {
                return;
            }

            PrefabUtility.SaveAsPrefabAsset(new GameObject(), string.Format("{0}@{1}.prefab", AssetDatabase.GetAssetPath(dBEntry).Replace(".asset", ""), key));
        }

        public static void CreatePrefab<T>(DBEntry dBEntry, System.Action<T> onAdding = null) where T : Component {

            CreatePrefab(dBEntry, "Prefab", true, onAdding);
        }

        public static void CreatePrefab<T>(DBEntry dBEntry, string key = "Prefab", bool addComponent = true, System.Action<T> onAdding = null) where T : Component {

            if (string.IsNullOrEmpty(key)) {
                return;
            }
 
            PrefabUtility.SaveAsPrefabAsset(new GameObject(), string.Format("{0}@{1}.prefab", AssetDatabase.GetAssetPath(dBEntry).Replace(".asset", ""), key));

            if (addComponent) {
                CreateOrAddComponentToPrefab(dBEntry, key, onAdding);
            }
        }

        public static void CreateOrAddComponentToPrefab<T>(DBEntry dbEntry, string key, System.Action<T> onAdding) where T : Component {

            var originalPrefab = dbEntry.Load<GameObject>(key);
#if UNITY_2018_3_OR_NEWER
            var path = AssetDatabase.GetAssetPath(originalPrefab);
            var prefabGO = PrefabUtility.LoadPrefabContents(path);
            var component = prefabGO.GetComponent<T>();
            if (component == null) {
                component = prefabGO.AddComponent<T>();
            }
            onAdding?.Invoke(component);
            PrefabUtility.SaveAsPrefabAsset(prefabGO, path);
#else
        var component = originalPrefab.GetComponent<ComponentType>();
        if (component == null) {
            component = originalPrefab.AddComponent<ComponentType>();
        }
        onAdding.SafeInvoke(component);
#endif
            EditorUtility.SetDirty(originalPrefab);
        }
    }
}