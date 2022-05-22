using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    /**
     * <summary>Main class for resource management subsystem. Has link to <see cref="DBCollection"/> where all paths to
     * DBEntries (extended ScriptableObject) stored. When first time <see cref="Get{T}()"/> or <see cref="GetAll{T}()"/>
     * called that paths converts to links to assets (of type DBEntry). From DBEntry any UnityObjects can be loaded with
     * Load<T> method. To collect all DBEntry paths in projects do Assist > Collect items in Unity. All DBEntry scriptable
     * objects should be in or under Resources folders.</summary>
     */
    public static class DB {

        #region Private Fields

        private static Dictionary<DBEntry, string> _items = new Dictionary<DBEntry, string>();

        private static DBCollection _instance;

        #endregion

        #region Class Implementation

        private static void CheckItems() {
            if (_instance == null) {
                _instance = Resources.Load<DBCollection>("DB");
            }

            if (_instance == null) {
                return;
            }

            if (_items == null) {
                _items = new Dictionary<DBEntry, string>();
            } else {
                //_instance._items.Clear();
            }

            if (_items.Count < _instance.paths.Length) {
                _instance.paths.SelectNotNull().ToList().ForEach(path => {
                    var dbItem = Resources.Load<DBEntry>(path);
                    if (dbItem != null && !_items.ContainsKey(dbItem)) {
                        _items.Add(dbItem, path);
                    }
                });
            }
        }

        public static T Get<T>(string name) where T : DBEntry {
            CheckItems();
            return GetAll<T>().SelectNotNull().FirstOrDefault(item => item != null && item.name == name);
        }

        public static T Get<T>() where T : DBEntry {
            CheckItems();
            return GetAll<T>().SelectNotNull().First();
        }

        public static DBEntry Get(Type type) {
            CheckItems();
            return GetAll<DBEntry>().FirstOrDefault(type.IsInstanceOfType);
        }

        public static List<T> GetAll<T>() where T : DBEntry {
            CheckItems();
            var items = new List<T>();
            if (_instance == null) {
                Debug.LogError("No DB instance");
                return items;
            }

            return _items.Keys.OfType<T>().ToList();
        }

        public static List<T> GetAll<T>(Func<T, bool> selector) where T : DBEntry {
            var allOfType = GetAll<T>();

            return allOfType.Where(selector).ToList();
        }

        public static string GetPath(DBEntry item) {
            CheckItems();
            return _items.ContainsKey(item) ? _items[item] : null;
        }

        #endregion
    }
}