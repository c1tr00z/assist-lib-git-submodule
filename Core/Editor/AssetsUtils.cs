using System;
using UnityEditor;
using UnityEngine;

public static class AssetsUtils {

    public static T CreateScriptableObject<T>(string path) where T : ScriptableObject {
        return CreateScriptableObject<T>(path, typeof(T).ToString(), false);
    }

    public static T CreateScriptableObject<T>(string path, string name) where T : ScriptableObject {
        return CreateScriptableObject<T>(path, name, false);
    }

    public static T CreateScriptableObject<T>(string path, string name, bool select) where T : ScriptableObject {
        return (T)CreateScriptableObject(typeof(T), path, name, select);
    }

    public static ScriptableObject CreateScriptableObject(Type type, string path, string name) {
        return CreateScriptableObject(type, path, name, false);
    }

    public static ScriptableObject CreateScriptableObject(Type type, string path, string name, bool select) {
        ScriptableObject item = ScriptableObject.CreateInstance(type);

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/{name}.asset");

        AssetDatabase.CreateAsset(item, assetPathAndName);

        AssetDatabase.SaveAssets();

        if (select) {
            Selection.activeObject = item;
        }

        return item;
    }

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
        onAdding.SafeInvoke(component);
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
