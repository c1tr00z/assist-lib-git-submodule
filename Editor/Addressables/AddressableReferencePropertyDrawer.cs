using System;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.Addressables {
    public class AddressableReferencePropertyDrawer : PropertyDrawer {

        #region Readonly Fields

        private static readonly string PROPERTY_GUID = "guid";
        private static readonly string PROPERTY_ADDRESS = "address";

        #endregion

        #region Private Fields

        private AddressableAssetSettings _settings;

        #endregion

        #region Accessors

        private AddressableAssetSettings settings =>
            CommonUtils.GetCachedObject(ref _settings, () => AddressableAssetSettingsDefaultObject.Settings);

        #endregion

        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);

            var guidProperty = property.FindPropertyRelative(PROPERTY_GUID);
            var addressProperty = property.FindPropertyRelative(PROPERTY_GUID);

            var addressableAttribute = attribute as AddressableTypeAttribute;

            var filteredType = addressableAttribute.assetType;

            Object asset = null;
            var guid = guidProperty.stringValue;
            var address = addressProperty.stringValue;
            var groupName = string.Empty;

            if (TryFindEntry(guid, address, out AddressableAssetGroup group, out AddressableAssetEntry entry)) {
                asset = entry.MainAsset;
                guid = entry.guid;
                address = entry.address;
                groupName = group.Name;
            } else {
                guid = string.Empty;
                address = string.Empty;
            }

            var objectRect = new Rect(position.x, position.y, position.width, 16);
            var newAsset = EditorGUI.ObjectField(objectRect, asset, filteredType, false);

            if (newAsset != asset) {
                guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(newAsset));
                if (TryFindEntry(guid, null, out group, out entry)) {
                    guid = entry.guid;
                    address = entry.address;
                    groupName = group.Name;
                } else {
                    if (newAsset != null) {
                        Debug.LogError($"Asset [{guid}] not found or not addressable");
                    }
                    guid = string.Empty;
                    address = string.Empty;
                    groupName = string.Empty;
                }
                
                guidProperty.stringValue = guid;
                addressProperty.stringValue = address;
            }

            var guidRect = new Rect(objectRect.x, objectRect.y + 20, objectRect.width, 16);
            GUI.Label(guidRect, $"GUID: {guid}");
            var addressRect = new Rect(guidRect.x, guidRect.y + 20, guidRect.width, 16);
            GUI.Label(addressRect, $"Address/key: {address}");
            var groupRect = new Rect(addressRect.x, addressRect.y + 20, addressRect.width, 16);
            GUI.Label(groupRect, $"Group: {groupName}");

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 80;
        }

        #endregion

        #region Class Implementation

        private bool TryFindEntry(string guid, string address, out AddressableAssetGroup group,
            out AddressableAssetEntry entry) {

            var groups = settings.groups;

            if (!guid.IsNullOrEmpty() || !address.IsNullOrEmpty()) {
                if (!guid.IsNullOrEmpty()) {
                    foreach (var assetGroup in groups) {
                        var foundEntry = assetGroup.entries.FirstOrDefault(g => g.guid == guid);
                        if (foundEntry != null) {
                            group = assetGroup;
                            entry = foundEntry;
                            return true;
                        }
                    }
                }

                if (!address.IsNullOrEmpty()) {
                    foreach (var assetGroup in groups) {
                        var foundEntry = assetGroup.entries.FirstOrDefault(g => g.address == address);
                        if (foundEntry != null) {
                            group = assetGroup;
                            entry = foundEntry;
                            return true;
                        }
                    }
                }
            }

            group = null;
            entry = null;
            return false;
        }

        #endregion
    }
}