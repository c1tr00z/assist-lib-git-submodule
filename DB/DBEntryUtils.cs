using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Localization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.ResourcesManagement {

    public static class DBEntryUtils {

        #region Private Fields

        private static Dictionary<Type, DBEntry> _singletones = new Dictionary<Type, DBEntry>();

        #endregion

        #region Class Implementations

        public static string GetPath(this DBEntry item) {
            return DB.GetPath(item);
        }

        public static T Load<T>(this DBEntry item, string key) where T : Object {
            return (T) Resources.Load(GetPath(item) + "@" + key, typeof(T));
        }

        public static T LoadPrefab<T>(this DBEntry item) {
            return (T) (object) Resources.Load(GetPath(item) + "@Prefab", typeof(T));
        }

        public static string LoadText(this DBEntry item) {
            var textAsset = (TextAsset) Resources.Load(GetPath(item) + "@Text", typeof(TextAsset));
            return textAsset.text;
        }

        public static SpriteRenderer LoadSpriteRenderer(this DBEntry item, string key) {
            return Load<SpriteRenderer>(item, key);
        }

        public static Sprite LoadSprite(this DBEntry item, string key) {
            return item.Load<Sprite>(key);
        }

        public static Sprite LoadIcon(this DBEntry item) {
            return item.LoadSprite("Icon");
        }

        public static T GetCached<T>(ref T cachedDBEntry, string key = null) where T : DBEntry {
            if (cachedDBEntry.IsAssigned()) {
                return cachedDBEntry;
            }

            if (key.IsNullOrEmpty()) {
                cachedDBEntry = DB.Get<T>(key);
            } else {
                cachedDBEntry = Get<T>();
            }

            return cachedDBEntry;
        }

        public static T Get<T>() where T : DBEntry {
            var type = typeof(T);
            if (!_singletones.ContainsKey(type)) {
                var singleton = DB.Get<T>();
                _singletones.Add(type, singleton);
            }

            return (T)_singletones[type];
        }

        #endregion
    }
}