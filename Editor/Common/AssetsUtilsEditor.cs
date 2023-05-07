using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public static class AssetsUtilsEditor {

        #region Class Implementation

        public static TAsset FindAssetByName<TAsset>(string assetName) where TAsset : Object {
            var assetGUID = AssetDatabase.FindAssets($"t:{typeof(TAsset).Name} {assetName}").FirstOrDefault();

            if (assetGUID.IsNullOrEmpty()) {
                return null;
            }

            return LoadAssetByGUID<TAsset>(assetGUID);
        }

        public static TAsset LoadAssetByGUID<TAsset>(string guid) where TAsset : Object {
            return AssetDatabase.LoadAssetAtPath<TAsset>(AssetDatabase.GUIDToAssetPath(guid));
        }

        #endregion
    }
}