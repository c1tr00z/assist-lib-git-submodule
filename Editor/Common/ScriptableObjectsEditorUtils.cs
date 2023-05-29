using System;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
 
    public static class ScriptableObjectsEditorUtils {

        #region Class Implementation

        public static T Create<T>(string path, string name) where T : ScriptableObject {
            return Create<T>(path, name, false);
        }

        public static T Create<T>(string path, string name, bool select) where T : ScriptableObject {
            return (T)Create(typeof(T), path, name, select);
        }

        public static ScriptableObject Create(Type type, string path, string name) {
            return Create(type, path, name, false);
        }

        public static ScriptableObject Create(Type type, string path, string name, bool select) {
            ScriptableObject item = ScriptableObject.CreateInstance(type);

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/{name}.asset");

            Debug.Log($"{path}/{name} =  {assetPathAndName}");
            AssetDatabase.CreateAsset(item, assetPathAndName);

            AssetDatabase.SaveAssets();

            if (select) {
                Selection.activeObject = item;
            }

            return item;
        }
        
        public static T Create<T>() where T : ScriptableObject {
            return Create<T>(string.Format("New {0}", typeof(T).Name));
        }

        public static T Create<T>(string name) where T : ScriptableObject {
            var path = SelectionUtils.GetSelectedPath();

            var item = Create<T>(path, name);

            Selection.activeObject = item;

            return item;
        }

        public static ScriptableObject Create(Type type, string name) {
            if (!type.IsSubclassOf(typeof(ScriptableObject))) {
                return null;
            }
            
            var path = SelectionUtils.GetSelectedPath();

            var item = Create(type, path, name);
            
            AssetDatabase.Refresh();

            Selection.activeObject = item;

            return item;
        }

        #endregion
    }
}