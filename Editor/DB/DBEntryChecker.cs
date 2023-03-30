using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace AssistLib.Editor.DB {
    public abstract class DBEntryChecker {

        #region Class Implementation

        public abstract void Check(DBEntry dbEntry, AddressableAssetSettings addressableSettings, string groupName);

        #endregion

    }

    public abstract class DBEntryChecker<T> : DBEntryChecker where T : DBEntry {

        #region DBEntryChecker Implementation

        public override void Check(DBEntry dbEntry, AddressableAssetSettings addressableSettings, string groupName) {
            if (!(dbEntry is T requiredItem)) {
                return;
            }
            
            CheckEntry(requiredItem, addressableSettings, groupName);
        }

        #endregion

        #region Class Implementation

        protected abstract void CheckEntry(T dbEntry, AddressableAssetSettings addressableSettings, string groupName);

        protected AddressableAssetEntry FindEntry<TAsset>(DBEntry dbEntry, string key,
            AddressableAssetSettings addressableSettings, string groupName) where TAsset : Object {

            var assetName = $"{dbEntry.name}@{key}";
            
            var guid = AssetDatabase.FindAssets($"t:{typeof(TAsset).Name} {assetName}").FirstOrDefault();
            if (guid.IsNullOrEmpty()) {
                return null;
            }

            var group = !groupName.IsNullOrEmpty()
                ? addressableSettings.FindGroup(groupName)
                : addressableSettings.DefaultGroup;

            if (group == null) {
                group = addressableSettings.CreateGroup(groupName, false, false, false,
                    new List<AddressableAssetGroupSchema>());
            }

            var entry = addressableSettings.FindAssetEntry(guid);
            if (entry == null) {
                entry = addressableSettings.CreateOrMoveEntry(guid, group);
            }

            entry.address = assetName;

            return entry;
        }

        #endregion

    }
}