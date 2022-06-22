using System;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.Common;
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
        public static string GetPath(this DBEntry dbEntry) {
            return DB.GetPath(dbEntry);
        }

        private static IEnumerator C_LoadAsync<T>(DBEntry dbEntry, string key, AssetRequest<T> request) where T : Object {
            var resourceRequest = Resources.LoadAsync<T>($"{GetPath(dbEntry)}@{key}");

            yield return resourceRequest;
            
            request.AssetLoaded(resourceRequest.asset as T);
        }

        /**
         * <summary>Loads any UnityObjects for DBEntry. Object should be in same folder as DBEntry and have name X@Y
         * where X is DBEntry name and Y is any desirable key (for example Player@Icon or Hammer@Model</summary>
         */
        public static AssetRequest<T> LoadAsync<T>(this DBEntry dbEntry, string key) where T : Object {
            var request = new AssetRequest<T>();
            
            CoroutineStarter.RequestCoroutine(C_LoadAsync(dbEntry, key, request));

            return request;
        }

        private static IEnumerator C_LoadAsync<T>(DBEntry dbEntry, string key, Action<T> callback) where T : Object {
            var request = LoadAsync<T>(dbEntry, key);

            yield return request;
            
            callback?.Invoke(request.asset as T);
        }

        /**
         * <summary>Loads any UnityObjects for DBEntry. Object should be in same folder as DBEntry and have name X@Y
         * where X is DBEntry name and Y is any desirable key (for example Player@Icon or Hammer@Model</summary>
         */
        public static void LoadAsync<T>(this DBEntry dbEntry, string key, Action<T> callback) where T : Object {
            CoroutineStarter.RequestCoroutine(C_LoadAsync(dbEntry, key, callback));
        }

        /**
         * <summary>Loads prefab associated with DBEntry. Prefab should have name X@Prefab where X is DBEntry name</summary>
         */
        public static AssetRequest<T> LoadPrefabAsync<T>(this DBEntry dbEntry) where T : Object {
            return dbEntry.LoadAsync<T>("Prefab");
        }

        /**
         * <summary>Loads prefab associated with DBEntry. Prefab should have name X@Prefab where X is DBEntry name</summary>
         */
        public static void LoadPrefabAsync<T>(this DBEntry dbEntry, Action<T> callback) where T : Object {
            LoadAsync(dbEntry, "Prefab", callback);
        }

        /**
         * <summary>Loads content of TextAsset, associated with DBEntry and with name X@Text where X is DBEntry name</summary>
         */
        public static AssetRequest<TextAsset> LoadTextAsync(this DBEntry dbEntry) {
            return dbEntry.LoadAsync<TextAsset>("Text");
        }

        /**
         * <summary>Loads content of TextAsset, associated with DBEntry and with name X@Text where X is DBEntry name</summary>
         */
        public static void LoadTextAsync(this DBEntry dbEntry, Action<string> callback) {
            void OnLoad(TextAsset textAsset) {
                if (textAsset == null) {
                    return;
                }
                callback?.Invoke(textAsset.text);
            }
            dbEntry.LoadAsync<TextAsset>("Text", OnLoad);
        }

        /**
         * <summary>Loads SpriteRenderer associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
         * <param name="dbEntry">DBEntry</param>
         * <param name="key">Key for SpriteRenderer name</param>
         */
        public static AssetRequest<SpriteRenderer> LoadSpriteRendererAsync(this DBEntry dbEntry, string key) {
            return dbEntry.LoadAsync<SpriteRenderer>(key);
        }

        /**
         * <summary>Loads SpriteRenderer associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
         * <param name="dbEntry">DBEntry</param>
         * <param name="key">Key for SpriteRenderer name</param>
         */
        public static void LoadSpriteRendererAsync(this DBEntry dbEntry, string key, Action<SpriteRenderer> callback) {
            dbEntry.LoadAsync(key, callback);
        }

        /**
         * <summary>Loads Sprite associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
         * <param name="dbEntry">DBEntry</param>
         * <param name="key">Key for Sprite name</param>
         */
        public static AssetRequest<Sprite> LoadSpriteAsync(this DBEntry dbEntry, string key) {
            return dbEntry.LoadAsync<Sprite>(key);
        }

        /**
         * <summary>Loads Sprite associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
         * <param name="dbEntry">DBEntry</param>
         * <param name="key">Key for Sprite name</param>
         * <param name="callback">Callback</param>
         */
        public static void LoadSpriteAsync(this DBEntry dbEntry, string key, Action<Sprite> callback) {
            dbEntry.LoadAsync(key, callback);
        }

        /**
         * <summary>Loads Sprite icon associated with DBEntry and with name X@Icon where X is DBEntry name</summary>
         * <param name="item">DBEntry</param>
         */
        public static AssetRequest<Sprite> LoadIconAsync(this DBEntry item) {
            return item.LoadSpriteAsync("Icon");
        }

        public static void LoadIconAsync(this DBEntry item, Action<Sprite> callback) {
            item.LoadAsync("Icon", callback);
        }

        /**
         * <summary>Returns cached (if possible) DBEntry by type and key</summary>
         */
        public static T GetCached<T>(ref T cachedDBEntry, string key = null) where T : DBEntry {
            if (!cachedDBEntry.IsNull()) {
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

        public static T GetDBEntry<T>(this Component component, ref DBEntryResource dbEntryResource) where T : DBEntry {
            return component.GetCachedComponent(ref dbEntryResource).parent as T;
        }

        public static T GetDBEntry<T>(this Component component, ref T dbEntry) where T : DBEntry {
            if (dbEntry == null) {
                dbEntry = component.GetComponent<DBEntryResource>().parent as T;
            }

            return dbEntry;
        }

        #endregion
    }
}