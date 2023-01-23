using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.Utils {
    public static class EditorGUIUtils {

        #region Class Implementation

        public static float GetDisplayNameFieldWidth(float fieldWidth) {
            float minPropertyWidth = 250f;
            float minDisplayNameWidth = 150f;
            float displayNameScale = .42f;

            return fieldWidth < minPropertyWidth ? minDisplayNameWidth : fieldWidth * displayNameScale;
        }

        public static bool RefreshButton() {
            return GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Refresh"), GUILayout.Width(30));
        }

        public static T Field<T>(Rect rect, string label, T currentValue) {
            var type = typeof(T);

            return (T)Field(type, rect, label, currentValue);
        }

        public static object Field(Type type, Rect rect, string label, object currentValue) {
            if (type == typeof(string)) {
                var currentString = currentValue.IsNull() ? string.Empty : currentValue.ToString();
                return EditorGUI.TextField(rect, label, currentString);
            }

            if (type == typeof(Int32)) {
                var currentInt = (Int32)currentValue;
                return EditorGUI.IntField(rect, label, currentInt);
            }
            
            if (type == typeof(Int64)) {
                var currentLong = (Int64)currentValue;
                return EditorGUI.LongField(rect, label, currentLong);
            }
            
            if (type == typeof(bool)) {
                var currentBool = (bool)currentValue;
                return EditorGUI.Toggle(rect, label, currentBool);
            }

            if (type == typeof(float)) {
                var currentFloat = (Single)currentValue;
                return EditorGUI.FloatField(rect, label, currentFloat);
            }
            
            if (type == typeof(double)) {
                var currentFloat = (float)currentValue;
                return EditorGUI.FloatField(rect, label, currentFloat);
            }
            
            if (type == typeof(Vector4)) {
                var currentVector = (Vector4)currentValue;
                return EditorGUI.Vector4Field(rect, label, currentVector);
            }
            
            if (type == typeof(Vector3)) {
                var currentVector = (Vector3)currentValue;
                return EditorGUI.Vector3Field(rect, label, currentVector);
            }

            if (type == typeof(Vector2)) {
                var currentVector = (Vector2)currentValue;
                return EditorGUI.Vector2Field(rect, label, currentVector);
            }
            
            if (type == typeof(Vector3Int)) {
                var currentVector = (Vector3Int)currentValue;
                return EditorGUI.Vector3IntField(rect, label, currentVector);
            }

            if (type == typeof(Vector2Int)) {
                var currentVector = (Vector2Int)currentValue;
                return EditorGUI.Vector2IntField(rect, label, currentVector);
            }
            
            if (typeof(Object).IsAssignableFrom(type)) {
                var currentObject = (Object)currentValue;
                return EditorGUI.ObjectField(rect, label, currentObject, type, false);
            }
            
            Debug.LogError($"No editor fields for type {type}");
            return null;
        }

        #endregion

    }
}
