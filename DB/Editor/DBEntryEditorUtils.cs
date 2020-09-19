using System;
using System.Collections.Generic;
using System.IO;
using c1tr00z.AssistLib.Common;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.DataBase.Editor {

    public static class DBEntryEditorUtils {

        [MenuItem("Assets/Create DBEntry")]
        public static void CreateDBEntry() {
            CreateItem<DBEntry>();
        }

        [MenuItem("Assist/Create DB")]
        public static void CreateDB() {
            if (DB.Get<global::DB>() != null) {
                return;
            }

            PathUtils.CreatePath("Resources");
            ScriptableObjectsEditorUtils.Create<DB>(PathUtils.Combine("Assets", "Resources"), "DB");
            CollectItems();
        }

        [MenuItem("Assist/Create AppSettings")]
        public static void CreateAppSettings() {
            if (DB.Get<DB>() == null) {
                return;
            }

            if (DB.Get<AppSettings>() != null) {
                return;
            }

            PathUtils.CreatePath("AssistLib", "Resources");
            CreateItem<AppSettings>(PathUtils.Combine("Assets", "AssistLib", "Resources"),
                "AppSettings");
            CollectItems();
        }

        [MenuItem("Assist/Collect items")]
        public static void CollectItems() {
            var itemsObject = Resources.Load<DB>("DB");
            var dirs = new List<string>();
            var items = Resources.LoadAll<DBEntry>("");
            var newItemsPaths = items.Select(i => {
                Debug.Log($"CollectItems: {i}");
                var path = AssetDatabase.GetAssetPath(i).Replace(".asset", "");
                Debug.Log($"CollectItems: {path}");
                path = (path.Contains("Resources"))
                    ? path.Replace(path.Substring(0, path.IndexOf("Resources") + "Resources/".Length), "")
                    : path;
                return path;
            }).ToArray();
            // if (itemsObject.paths.Length != newItemsPaths.Length) {
                itemsObject.paths = newItemsPaths;
            // }

            foreach (DBEntry i in items) {
                var itemPrefab = i.LoadPrefab<GameObject>();
                if (itemPrefab != null) {

#if UNITY_2018_3_OR_NEWER
                    var save = false;
                    var path = AssetDatabase.GetAssetPath(itemPrefab);
                    var prefabGO = PrefabUtility.LoadPrefabContents(path);
                    var itemResource = prefabGO.GetComponent<DBEntryResource>();
                    if (itemResource == null) {
                        itemResource = prefabGO.AddComponent(typeof(DBEntryResource)) as DBEntryResource;
                        save = true;
                    }

                    if (!save && (itemResource.parent != i || itemResource.key != "Prefab")) {
                        itemResource.SetParent(i, "Prefab");
                        save = true;
                    }

                    if (save) {
                        try {
                            PrefabUtility.SaveAsPrefabAsset(prefabGO, path);
                            EditorUtility.SetDirty(itemPrefab);
                        }
                        catch (Exception e) {
                            Debug.LogError(e);
                        }
                    }
#else
                var itemResource = itemPrefab.GetComponent<DBEntryResource>();
                if (itemResource == null) {
                    itemResource = itemPrefab.AddComponent(typeof(DBEntryResource)) as DBEntryResource;
                }

                itemResource.SetParent(i, "Prefab");
                EditorUtility.SetDirty(itemPrefab);
#endif
                }

            }

            EditorUtility.SetDirty(itemsObject);
        }

        public static void GetDirectories(string startPath, string path, List<string> directories) {
            var info = new DirectoryInfo(path);
            var dirs = info.GetDirectories();
            foreach (DirectoryInfo d in dirs) {
                var dir = d.ToString();
                var newDir = dir.Replace(path, "");
                newDir = newDir.StartsWith("Resources") ? newDir.Substring("Resources".Length) : newDir;
                newDir = newDir.StartsWith("\\") ? newDir.Substring("\\".Length) : newDir;
                if (!string.IsNullOrEmpty(newDir)) {
                    directories.Add(newDir);
                }

                GetDirectories(startPath, dir, directories);
            }
        }

        public static void AutoCollect() {
            if (DBSettings.autoCollect) {
                CollectItems();
            }
        }

        public static T CreateItem<T>() where T : DBEntry {

            var item = ScriptableObjectsEditorUtils.Create<T>();
            
            CollectItems();

            return item;
        }
        
        public static T CreateItem<T>(string path, string name) where T : DBEntry {

            var item = ScriptableObjectsEditorUtils.Create<T>(path, name);
            
            CollectItems();

            return item;
        }
    }
}

