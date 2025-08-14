using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Addressables;
using c1tr00z.AssistLib.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.ResourcesManagement {

    /// <summary>
    /// Extension method class for DBEntry type
    /// </summary>
    public static class DBEntryUtils {

        #region Private Fields

        private static Dictionary<Type, DBEntry> _singletones = new Dictionary<Type, DBEntry>();

        #endregion

        #region Class Implementations

        /// <summary>
        /// Returns relative path to DBEntry
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <returns></returns>
        public static string GetPath(this DBEntry dbEntry) {
            return DB.GetPath(dbEntry);
        }

        public static async UniTask<T> LoadAsync<T>(this AddressableReference reference)
            where T : Object {

            if (reference.TryGetLoadedAsset(out T exist)) {
                return exist;
            }

            var wait = true;

            IResourceLocation location = default;
            void callback(IResourceLocation foundLocation) {
                location = foundLocation;
                wait = false;
            };
            
            reference.LoadIResourceLocation(callback);

            while (wait) {
                await UniTask.DelayFrame(1);
            }
            
            if (location == default) {
                return null;
            }
            
            var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(location);

            await handle;
            
            reference.SaveLoadedAsset(handle.Result);

            return handle.Result;
        }

        /// <summary>
        /// Loads any UnityObjects for DBEntry. Object should be in same folder as DBEntry and have name X@Y
        /// is DBEntry name and Y is any desirable key (for example Player@Icon or Hammer@Model
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async static UniTask<T> LoadAsync<T>(this DBEntry dbEntry, string key) where T : Object {
            var reference = AddressableUtils.MakeFromAddress($"{dbEntry.name}@{key}");
            
            var result = await LoadAsync<T>(reference);

            return result;
        }

        /// <summary>
        /// Loads prefab associated with DBEntry. Prefab should have name X@Prefab where X is DBEntry name
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> LoadPrefabAsync<T>(this DBEntry dbEntry) where T : Object{
            return await LoadAsync<T>(dbEntry, "Prefab");
        }

        public static async UniTask<T> InstantiateAsync<T>(this DBEntry dbEntry, string key) where T : Object {
            var reference = AddressableUtils.MakeFromAddress($"{dbEntry.name}@{key}");

            var instance = await InstantiateAsync<T>(reference);

            return instance;
        }

        public static async UniTask<T> InstantiateAsync<T>(AddressableReference reference) where T : Object {
            var wait = true;
            IResourceLocation location = default;
            void locationCallback(IResourceLocation foundLocation) {
                location = foundLocation;
                wait = false;
            };
            
            reference.LoadIResourceLocation(locationCallback);

            while (wait) {
                await UniTask.DelayFrame(1);
            }
            
            var handledObject = await UnityEngine.AddressableAssets.Addressables.InstantiateAsync(location);

            if (typeof(GameObject).IsAssignableFrom(typeof(T))) {
                return handledObject as T;
            }

            return handledObject.GetComponent<T>();
        }
        
        public static async UniTask<T> InstantiatePrefabAsync<T>(this DBEntry dbEntry) where T : Object {
            var prefab = await dbEntry.InstantiateAsync<T>("Prefab");
            return prefab;
        }
        
        /// <summary>
        /// Loads content of TextAsset, associated with DBEntry and with name X@Text where X is DBEntry name
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <returns></returns>
        public static UniTask<TextAsset> LoadTextAsync(this DBEntry dbEntry) {
            return dbEntry.LoadAsync<TextAsset>("Text");
        }

        /// <summary>
        /// <summary>Loads SpriteRenderer associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
        /// </summary>
        /// <param name="dbEntry">DBEntry</param>
        /// <param name="key">Key for SpriteRenderer name</param>
        /// <returns></returns>
        public static async UniTask<SpriteRenderer> LoadSpriteRendererAsync(this DBEntry dbEntry, string key) {
            var spriteRenderer = await dbEntry.LoadAsync<SpriteRenderer>(key);
            return spriteRenderer;
        }

        /// <summary>
        /// * <summary>Loads Sprite associated with DBEntry and with name X@Y where X is DBEntry name and Y is key</summary>
        /// </summary>
        /// <param name="dbEntry">DBEntry</param>
        /// <param name="key">Key for Sprite name</param>
        /// <returns></returns>
        public static async UniTask<Sprite> LoadSpriteAsync(this DBEntry dbEntry, string key) {
            var assetName = $"{dbEntry.name}@{key}[{dbEntry.name}@{key}]";
            var sprite = await LoadAsync<Sprite>(AddressableUtils.MakeFromAddress(assetName));
            return sprite;
        }

        /// <summary>
        /// <summary>Loads Sprite icon associated with DBEntry and with name X@Icon where X is DBEntry name</summary>
        /// </summary>
        /// <param name="item">DBEntry</param>
        /// <returns></returns>
        public static async UniTask<Sprite> LoadIconAsync(this DBEntry item) {
            var icon = await item.LoadSpriteAsync("Icon");
            return icon;
        }

        /// <summary>
        /// Returns cached (if possible) DBEntry by type and key
        /// </summary>
        /// <param name="cachedDBEntry">Cached DBEntry object</param>
        /// <param name="key">Key</param>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Returns cached DBEntry by type from singletons list
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : DBEntry {
            var type = typeof(T);
            if (!_singletones.ContainsKey(type)) {
                var singleton = DB.Get<T>();
                _singletones.Add(type, singleton);
            }

            return (T)_singletones[type];
        }

        public static T GetDBEntry<T>(this Component component, ref T dbEntry) where T : DBEntry {
            if (dbEntry == null) {
                dbEntry = component.GetComponent<DBEntryResource>().parent as T;
            }

            return dbEntry;
        }

        public static T GetDBEntry<T>(this DBEntryReference dbEntryRef) where T : DBEntry {
            return DB.Get<T>(dbEntryRef.dbEntryName);
        }

        public static bool IsValid(this DBEntryReference dbEntryRef) {
            return !dbEntryRef.dbEntryName.IsNullOrEmpty();
        }

        public static bool TryDownloadedAsset<T>(this DBEntry dbEntry, string key, out T asset) where T : Object {
            var reference = AddressableUtils.MakeFromAddress($"{dbEntry.name}@{key}");
            return reference.TryGetLoadedAsset(out asset);
        }


        #endregion
    }
}