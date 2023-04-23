using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace AssistLib.Utils.Editor {
    public static class ObjectEditorUtils {

        #region Class Implementation

        public static T CloneEditor<T>(this T prefab, Transform parent = null) where T : Component {
            var instance = (T) PrefabUtility.InstantiatePrefab(prefab);
            if (parent != null) {
                instance.transform.Reset(parent);
            }
            return instance;
        }

        public static T LoadByName<T>(string name) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(
                AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets($"t:{typeof(T).Name} {name}").FirstOrDefault()));
        }
        
        #endregion
    }
}