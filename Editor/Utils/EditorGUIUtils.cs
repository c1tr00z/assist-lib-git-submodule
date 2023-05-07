using System;
using System.Collections.Generic;
using AssistLib.Editor.Utils;
using AssistLib.Utils.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.Utils {
    public static class EditorGUIUtils {

        #region Accessors

        public static int indentLevel { get; private set; }

        public static int indentSpace => 16;

        #endregion

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
        
        public static bool ConsoleButton() {
            return GUILayout.Button(EditorGUIUtility.IconContent("UnityEditor.ConsoleWindow"), GUILayout.Width(24));
        }
        
        public static bool PlusButton() {
            return GUILayout.Button("+", GUILayout.Width(24));
        }

        public static bool RemoveButton() {
            return GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash"), GUILayout.Width(24));
        }

        public static bool ButtonUp() {
            return GUILayout.Button("▲", GUILayout.Width(24));
        }
        
        public static bool ButtonDown() {
            return GUILayout.Button("▼", GUILayout.Width(24));
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
        
        public static void ListField<T>(EditorGUIListData<T> listData) {
            listData.showList = ListField(listData.label, ref listData.items, listData.showList, listData.allowSceneObjects);
        }
        
        public static bool ListField<T>(string label, ref List<T> currentList, bool show = true, bool allowSceneObjects = false) {
            var toDelete = new List<int>();
            EditorGUILayout.BeginHorizontal();
            show = EditorGUILayout.Foldout(show, label);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            var sizeDiff = EditorGUILayout.IntField("Size", currentList.Count) - currentList.Count;
            if (sizeDiff > 0) {
                for (int i = 0; i < sizeDiff; i++) {
                    currentList.Add(Activator.CreateInstance<T>());
                }
            } else if (sizeDiff < 0) {
                for (int i = sizeDiff; i < 0; i++) {
                    toDelete.Add(currentList.Count + sizeDiff);
                }
            }
            if (GUILayout.Button("+", GUILayout.Width(24))) {
                if (typeof(Object).IsAssignableFrom(typeof(T))) {
                    currentList.Add(default);
                } else {
                    currentList.Add(Activator.CreateInstance<T>());
                }
            }
            EditorGUILayout.EndHorizontal();
            if (show) {
                for (int i = 0; i < currentList.Count; i++) {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label($"#{i}", GUILayout.Width(32));
                    currentList[i] = ObjectFieldByType(currentList[i], false, null, allowSceneObjects);
                    if (RemoveButton()) {
                        toDelete.Add(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            foreach (var i in toDelete) {
                if (i < currentList.Count) {
                    currentList.RemoveAt(i);
                }
            }

            return show;
        }
        
        private static T ObjectFieldByType<T>(string label, T value) {
            return ObjectFieldByType(value, true, label);
        }

        private static T ObjectFieldByType<T>(T value, bool useLabel = false, string label = null, bool allowSceneObjects = false) {
            var targetType = typeof(T);
            if (typeof(Single).IsAssignableFrom(targetType)) {
                var singleValue = (Single)(object)value;
                return useLabel
                    ? (T)(object)EditorGUILayout.FloatField(label, singleValue)
                    : (T)(object)EditorGUILayout.FloatField(singleValue);
            }

            if (typeof(Int32).IsAssignableFrom(targetType)) {
                var int32Value = (Int32)(object)value;
                return useLabel 
                    ? (T)(object)EditorGUILayout.IntField(label, int32Value)
                    : (T)(object)EditorGUILayout.IntField(int32Value);
            }

            if (typeof(Int64).IsAssignableFrom(targetType)) {
                var int64Value = (Int64)(object)value;
                return useLabel
                    ? (T)(object)EditorGUILayout.LongField(label, int64Value)
                    : (T)(object)EditorGUILayout.LongField(int64Value);
            }
            
            if (typeof(string).IsAssignableFrom(targetType)) {
                var stringValue = value != null ? value.ToString() : null;
                return useLabel
                    ? (T)(object)EditorGUILayout.TextField(label, stringValue)
                    : (T)(object)EditorGUILayout.TextField(stringValue);
            }
            
            if (typeof(Vector2).IsAssignableFrom(targetType)) {
                var vector2Value = (Vector2)(object)value;
                return useLabel
                    ? (T)(object)EditorGUILayout.Vector2Field(label, vector2Value)
                    : (T)(object)EditorGUILayout.Vector2Field("Vector2", vector2Value);
            }
            
            if (typeof(Vector3).IsAssignableFrom(targetType)) {
                var vector3Value = (Vector3)(object)value;
                return useLabel
                    ? (T)(object)EditorGUILayout.Vector3Field(label, vector3Value)
                    : (T)(object)EditorGUILayout.Vector3Field("Vector3", vector3Value);
            }
            
            if (typeof(Vector4).IsAssignableFrom(targetType)) {
                var vector4Value = (Vector4)(object)value;
                return useLabel
                    ? (T)(object)EditorGUILayout.Vector4Field(label, vector4Value)
                    : (T)(object)EditorGUILayout.Vector4Field("Vector4", vector4Value);
            }
            
            if (typeof(Object).IsAssignableFrom(targetType)) {
                var objectValue = value as Object;
                return useLabel
                    ? (T)(object)EditorGUILayout.ObjectField(label, objectValue, typeof(T), allowSceneObjects)
                    : (T)(object)EditorGUILayout.ObjectField(objectValue, typeof(T), allowSceneObjects);
            }

            EditorGUILayout.HelpBox($"Unknown type {value.GetType()}", MessageType.Error);
            return default;
        }
        
        public static void PingObject(this Object obj) {
            EditorGUIUtility.PingObject(obj);
        }
        
        public static void PingObjectByName(string name) {
            PingObject(ObjectEditorUtils.LoadByName<Object>(name));
        }
        
        public static void BeginIndentedBox() {
            indentLevel++;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentSpace * indentLevel);
            EditorGUILayout.BeginVertical();
        }

        public static void EndIndentedBox() {
            indentLevel--;
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        public static void BalanceIndentLevel() {
            indentLevel = 0;
        }

        #endregion

    }
}
