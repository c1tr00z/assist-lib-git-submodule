using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssistLib.Editor.DB;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.TypeReferences;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement.Editor {

    public static class DBEntryEditorActions {

        #region Class Implementation

        [MenuItem("Assets/Create DBEntry")]
        public static void CreateDBEntry() {
            CreateItem<DBEntry>();
        }

        [MenuItem("Assist/Create DB")]
        public static void CreateDB() {
            if (DB.Get<DBCollection>() != null) {
                return;
            }

            PathUtils.CreatePath("Resources");
            ScriptableObjectsEditorUtils.Create<DBCollection>(PathUtils.Combine("Assets", "Resources"), "DB");
            CollectItems();
        }

        [MenuItem("Assist/Collect items")]
        public static void CollectItems() {
            var itemsObject = Resources.Load<DBCollection>("DB");
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

            var addressableSettings = DBEntryEditorUtils.addressableSettings;

            var checkers = ReflectionUtils.GetSubclassesOf<DBEntryChecker>(false)
                .ToDictionary(t => t.BaseType.GetGenericArguments().FirstOrDefault(),
                    t => (DBEntryChecker)Activator.CreateInstance(t));

            var dbEntrySettings = DBEntryEditorUtils.LoadFromAssetDatabase<DBEntrySettings>("DBEntrySettings");
            var dbEntriesSettings =
                dbEntrySettings != null
                    ? dbEntrySettings.settings.ToDictionary(s => s.dbEntryType.GetRefType(), s => s.addressableGroupName)
                    : new Dictionary<Type, string>();

            foreach (DBEntry i in items) {

                var itemType = i.GetType();
                
                if (checkers.ContainsKey(itemType)) {
                    var groupName = dbEntriesSettings.ContainsKey(itemType) ? dbEntriesSettings[itemType] : null;
                    checkers[i.GetType()].Check(i, addressableSettings, groupName);
                }
                
                var itemPrefab = i.LoadPrefab<GameObject>();
                if (i.name.ToLower().Contains("witch")) {
                    Debug.LogError(itemPrefab);
                }
                if (itemPrefab != null) {

#if UNITY_2018_3_OR_NEWER

                    var prefabGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(itemPrefab));
                    var entry = addressableSettings.FindAssetEntry(prefabGUID);
                    if (entry == null) {
                        entry = addressableSettings.CreateOrMoveEntry(prefabGUID, addressableSettings.DefaultGroup);
                    }

                    if (entry.ReadOnly) {
                        entry.ReadOnly = false;
                    }

                    entry.address = $"{i.name}@Prefab";
                    
                    var save = false;
                    var path = AssetDatabase.GetAssetPath(itemPrefab);
                    var prefabGO = PrefabUtility.LoadPrefabContents(path);
                    var itemResource = prefabGO.GetComponent<DBEntryResource>();
                    if (itemResource == null) {
                        itemResource = prefabGO.AddComponent(typeof(DBEntryResource)) as DBEntryResource;
                        save = true;
                    }

                    if (itemResource.parent != i || itemResource.key != "Prefab") {
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
            if (true) {
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

        #endregion
    }
}

