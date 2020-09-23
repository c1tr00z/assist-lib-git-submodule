using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    public static class DB {
        
        private static Dictionary<DBEntry, string> _items { get; set; }

        private static DBCollection _instance;

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
                _instance.paths.SelectNotNull().ForEach(path => {
                    var dbItem = Resources.Load<DBEntry>(path);
                    if (dbItem != null) {
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

        public static List<T> GetAll<T>() where T : DBEntry {
            CheckItems();
            var items = new List<T>();
            if (_instance == null) {
                Debug.LogError("No DB instance");
                return items;
            }

            return _items.Keys.OfType<T>().ToList();
        }

        public static string GetPath(DBEntry item) {
            CheckItems();
            return _items.ContainsKey(item) ? _items[item] : null;
        }
    }
}