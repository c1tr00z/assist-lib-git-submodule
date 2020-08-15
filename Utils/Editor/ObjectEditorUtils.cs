using UnityEditor;
using UnityEngine;

namespace AssistLib.Utils.Editor {
    public static class ObjectEditorUtils {
        public static T CloneEditor<T>(this T prefab, Transform parent = null) where T : Component {
            var instance = (T) PrefabUtility.InstantiatePrefab(prefab);
            if (parent != null) {
                instance.transform.Reset(parent);
            }
            return instance;
        }
    }
}