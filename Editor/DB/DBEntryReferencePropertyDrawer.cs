using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace AssistLib.Editor.DB {
    [CustomPropertyDrawer(typeof(DBEntryReference))]
    public class DBEntryReferencePropertyDrawer : PropertyDrawer {

        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);
            
            var nameProperty = property.FindPropertyRelative("dbEntryName");
            var dbEntry = !nameProperty.stringValue.IsNullOrEmpty()
                ? c1tr00z.AssistLib.ResourcesManagement.DB.Get<DBEntry>(nameProperty.stringValue)
                : null;

            var newDBEntry = (DBEntry)EditorGUI.ObjectField(position, dbEntry, typeof(DBEntry), false);

            if (newDBEntry != dbEntry) {
                nameProperty.stringValue = newDBEntry != null ? newDBEntry.name : null;
            }
            
            EditorGUI.EndProperty();
        }

        #endregion
    }
}