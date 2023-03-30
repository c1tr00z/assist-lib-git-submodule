using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    public static class DBEntryEditorUtils {

        #region Private Fields

        private static AddressableAssetSettings _settings;

        #endregion

        #region Accessirs

        public static AddressableAssetSettings addressableSettings =>
            CommonUtils.GetCachedObject(ref _settings, () => AddressableAssetSettingsDefaultObject.Settings);

        #endregion
        
        #region Class Implementations

        private static T LoadAddressable<T>(DBEntry dbEntry, string key) where T : Object {
            var address = $"{dbEntry.name}@{key}";
            foreach (var group in addressableSettings.groups) {
                var entry = group.entries.FirstOrDefault(e => e.address.Contains(address));
                if (entry == null) {
                    continue;
                }

                return entry.MainAsset as T;
            }

            return null;
        }
        
        /**
         * 
         */
        public static T LoadFromAssetDatabase<T>(string assetName) where T : Object {
            var assetGUID = AssetDatabase.FindAssets($"t:{(typeof(T).Name)} {assetName}").FirstOrDefault();
            if (assetGUID.IsNullOrEmpty()) {
                return default;
            }
            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetGUID));
        }

        /**
         * 
         */
        private static T LoadFromAssetDatabase<T>(DBEntry dbEntry, string key) where T : Object {
            var assetName = $"{dbEntry.name}@{key}";
            return LoadFromAssetDatabase<T>(assetName);
        }

        /**
         * <summary>Loads any UnityObjects for DBEntry. Object should be in same folder as DBEntry and have name X@Y
         * where X is DBEntry name and Y is any desirable key (for example Player@Icon or Hammer@Model</summary>
         */
        public static T Load<T>(this DBEntry item, string key) where T : Object {
            var asset = LoadAddressable<T>(item, key);
            if (asset == null) {
                asset = LoadFromAssetDatabase<T>(item, key);
            }

            return asset;
        }

        /**
         * <summary>Loads prefab associated with DBEntry. Prefab should have name X@Prefab where X is DBEntry name</summary>
         */
        public static T LoadPrefab<T>(this DBEntry item) where T : Object {
            var prefab = LoadAddressable<T>(item, "Prefab");
            if (prefab == null) {
                prefab = LoadFromAssetDatabase<T>(item,"Prefab");
            }

            return prefab;
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

        #endregion
    }
}