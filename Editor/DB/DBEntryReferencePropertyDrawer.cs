using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace AssistLib.Editor.DB {
    [CustomPropertyDrawer(typeof(DBEntryTypeAttribute))]
    public class DBEntryReferencePropertyDrawer : PropertyDrawer {

        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);

            var type = typeof(DBEntry);
            if (attribute is DBEntryTypeAttribute typeAttribute) {
                type = typeAttribute.type ?? typeof(DBEntry);
            }
            
            var nameProperty = property.FindPropertyRelative("dbEntryName");
            var dbEntry = !nameProperty.stringValue.IsNullOrEmpty()
                ? c1tr00z.AssistLib.ResourcesManagement.DB.Get<DBEntry>(nameProperty.stringValue)
                : null;

            var newDBEntry = (DBEntry)EditorGUI.ObjectField(position, dbEntry, type, false);

            if (newDBEntry != dbEntry) {
                nameProperty.stringValue = newDBEntry != null ? newDBEntry.name : null;
            }
            
            EditorGUI.EndProperty();
        }

        #endregion
    }
}