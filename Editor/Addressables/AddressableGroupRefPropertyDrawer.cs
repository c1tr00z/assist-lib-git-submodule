using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace c1tr00z.AssistLib.Addressables {
    [CustomPropertyDrawer(typeof(AddressableGroupRef))]
    public class AddressableGroupRefPropertyDrawer : PropertyDrawer {

        #region Private Fields

        private List<AddressableAssetGroup> _groups = new List<AddressableAssetGroup>();

        private string[] _groupNames = Array.Empty<string>();

        #endregion

        #region Accessors
        
        private List<AddressableAssetGroup> groups {
            get {
                if (_groups.Count == 0) {
                    _groups = AddressableAssetSettingsDefaultObject.Settings.groups;
                }

                return _groups;
            }
        }

        private string[] groupNames {
            get {
                if (_groupNames.Length == 0) {
                    _groupNames = groups.SelectNotNull(g => g.name).ToArray();
                }

                return _groupNames;
            }
        }

        #endregion

        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, label);

            var groupNameProperty = property.FindPropertyRelative("groupName");

            var groupName = groupNameProperty.stringValue;
            var groupIndex = 0;
            if (groupName.IsNullOrEmpty()) {
                groupName = groups.FirstOrDefault()?.name;
                groupNameProperty.stringValue = groupName;
            } else {
                var currentGroup = groups.FirstOrDefault(g => g.name == groupName);
                if (currentGroup == null) {
                    currentGroup = groups.FirstOrDefault();
                }
                groupName = currentGroup.name;
                groupIndex = groups.IndexOf(currentGroup);
            }

            var popupRect = new Rect(position.x, position.y, position.width - 32, position.height);
            var newIndex = EditorGUI.Popup(popupRect, groupIndex, groupNames);
            if (newIndex != groupIndex) {
                groupNameProperty.stringValue = groupNames[newIndex];
            }
            
            var buttonRect = new Rect(position.x + position.width - 20, position.y, 20, position.height);
            if (GUI.Button(buttonRect, EditorGUIUtility.IconContent("TreeEditor.Refresh"))) {
                RefreshGroups();
            }
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 20;
        }

        #endregion

        #region Class Implementation

        private void RefreshGroups() {
            _groups = AddressableAssetSettingsDefaultObject.Settings.groups;
            _groupNames = groups.SelectNotNull(g => g.name).ToArray();
        }

        #endregion
    }
}