using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace AssistLib.Editor.DB {
    public abstract class DBEntryChecker {

        #region Class Implementation

        public abstract void Check(DBEntry dbEntry, AddressableAssetSettings addressableSettings);

        #endregion

    }

    public abstract class DBEntryChecker<T> : DBEntryChecker where T : DBEntry {

        #region DBEntryChecker Implementation

        public override void Check(DBEntry dbEntry, AddressableAssetSettings addressableSettings) {
            if (!(dbEntry is T requiredItem)) {
                return;
            }
            
            CheckEntry(requiredItem, addressableSettings);
        }

        #endregion

        #region Class Implementation

        protected abstract void CheckEntry(T dbEntry, AddressableAssetSettings addressableSettings);

        protected AddressableAssetEntry FindEntry<TAsset>(DBEntry dbEntry, string key,
            AddressableAssetSettings addressableSettings) where TAsset : Object {

            var assetName = $"{dbEntry.name}@{key}";
            
            var guid = AssetDatabase.FindAssets($"t:{typeof(TAsset).Name} {assetName}").FirstOrDefault();
            if (guid.IsNullOrEmpty()) {
                return null;
            }

            var entry = addressableSettings.FindAssetEntry(guid);
            if (entry == null) {
                entry = addressableSettings.CreateOrMoveEntry(guid, addressableSettings.DefaultGroup);
            }

            entry.address = assetName;

            return entry;
        }

        #endregion

    }
}