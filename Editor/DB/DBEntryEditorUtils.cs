using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    public static class DBEntryEditorUtils {

        #region Class Implementations

        /**
         * <summary>Loads any UnityObjects for DBEntry. Object should be in same folder as DBEntry and have name X@Y
         * where X is DBEntry name and Y is any desirable key (for example Player@Icon or Hammer@Model</summary>
         */
        public static T Load<T>(this DBEntry item, string key) where T : Object {
            return (T) Resources.Load(item.GetPath() + "@" + key, typeof(T));
        }

        /**
         * <summary>Loads prefab associated with DBEntry. Prefab should have name X@Prefab where X is DBEntry name</summary>
         */
        public static T LoadPrefab<T>(this DBEntry item) {
            return (T) (object) Resources.Load(item.GetPath() + "@Prefab", typeof(T));
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