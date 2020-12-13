using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.ResourcesManagement {

    /**
     * <summary>Extension method class for DBEntry type</summary>
     */
    public static class DBEntryUtils {

        #region Private Fields

        private static Dictionary<Type, DBEntry> _singletones = new Dictionary<Type, DBEntry>();

        #endregion

        #region Class Implementations

        /**
         * <summary>Returns relative path to DBEntry</summary>
         */
        public static string GetPath(this DBEntry item) {
            return DB.GetPath(item);
        }

        /**
         * <summary>Loads any UnityObjects for DBEntry. Object should be in same folder as DBEntry and have name X@Y
         * where X is DBEntry name and Y is any desirable key (for example Player@Icon or Hammer@Model</summary>
         */
        public static T Load<T>(this DBEntry item, string key) where T : Object {
            return (T) Resources.Load(GetPath(item) + "@" + key, typeof(T));
        }

        /**
         * <summary>Loads prefab associated with DBEntry. Prefab should have name X@Prefab where X is DBEntry name</summary>
         */
        public static T LoadPrefab<T>(this DBEntry item) {
            return (T) (object) Resources.Load(GetPath(item) + "@Prefab", typeof(T));
        }

        /**
         * <summary>Returns content of TextAsset, associated with DBEntry and with name X@Text where X is DBEntry name</summary>
         */
        public static string LoadText(this DBEntry item) {
            var textAsset = item.Load<TextAsset>("Text");
            return textAsset.text;
        }

        /**
         * <summary>Loads SpriteRenderer associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
         * <param name="item">DBEntry</param>
         * <param name="key">Key for SpriteRenderer name</param>
         */
        public static SpriteRenderer LoadSpriteRenderer(this DBEntry item, string key) {
            return Load<SpriteRenderer>(item, key);
        }

        /**
         * <summary>Loads Sprite associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
         * <param name="item">DBEntry</param>
         * <param name="key">Key for Sprite name</param>
         */
        public static Sprite LoadSprite(this DBEntry item, string key) {
            return item.Load<Sprite>(key);
        }

        /**
         * <summary>Loads Sprite icon associated with DBEntry and with name X@Icon where X is DBEntry name</summary>
         * <param name="item">DBEntry</param>
         */
        public static Sprite LoadIcon(this DBEntry item) {
            return item.LoadSprite("Icon");
        }

        /**
         * <summary>Returns cached (if possible) DBEntry by type and key</summary>
         */
        public static T GetCached<T>(ref T cachedDBEntry, string key = null) where T : DBEntry {
            if (cachedDBEntry.IsAssigned()) {
                return cachedDBEntry;
            }

            if (!key.IsNullOrEmpty()) {
                cachedDBEntry = DB.Get<T>(key);
            } else {
                cachedDBEntry = Get<T>();
            }

            return cachedDBEntry;
        }

        /**
         * <summary>Returns cached DBEntry by type from singletons list</summary>
         */
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