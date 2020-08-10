using System;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
 
    public static class ScriptableObjectsEditorUtils {

        public static T CreateObject<T>(string path) where T : ScriptableObject {
            return CreateObject<T>(path, typeof(T).ToString(), false);
        }

        public static T CreateObject<T>(string path, string name) where T : ScriptableObject {
            return CreateObject<T>(path, name, false);
        }

        public static T CreateObject<T>(string path, string name, bool select) where T : ScriptableObject {
            return (T)CreateObject(typeof(T), path, name, select);
        }

        public static ScriptableObject CreateObject(Type type, string path, string name) {
            return CreateObject(type, path, name, false);
        }

        public static ScriptableObject CreateObject(Type type, string path, string name, bool select) {
            ScriptableObject item = ScriptableObject.CreateInstance(type);

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/{name}.asset");

            AssetDatabase.CreateAsset(item, assetPathAndName);

            AssetDatabase.SaveAssets();

            if (select) {
                Selection.activeObject = item;
            }

            return item;
        }
    }
}