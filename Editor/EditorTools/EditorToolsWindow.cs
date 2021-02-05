using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    public class EditorToolsWindow : EditorWindow {

        #region Private Fields

        private EditorToolsController _controller;

        private int _selectedToolTypeIndex = 0;

        private string[] _typesNames = new string [0];

        #endregion

        #region Accessors

        private EditorToolsController controller {
            get {
                if (_controller == null) {
                    Load();
                }

                return _controller;
            }
        }

        public string[] typesNames {
            get {
                if (_typesNames == null || _typesNames.Length == 0) {
                    _typesNames = controller.editorToolsTypes.Select(t => t.FullName.Replace(".", "/")).ToArray();
                }

                return _typesNames;
            }
        }

        public int selectedToolTypeIndex {
            get {
                if (controller.editorToolsTypes.Count == 0 
                    || _selectedToolTypeIndex >= controller.editorToolsTypes.Count) {
                    _selectedToolTypeIndex = 0;
                    Debug.LogError(123);
                }

                return _selectedToolTypeIndex;
            }
            set => _selectedToolTypeIndex = value;
        }

        public Type selectedToolType {
            get {
                if (selectedToolTypeIndex >= controller.editorToolsTypes.Count) {
                    return null;
                }

                return controller.editorToolsTypes[_selectedToolTypeIndex];
            }
        }

        #endregion

        #region Unity Events

        private void OnEnable() {
            controller.Load();
        }

        private void OnDisable() {
            controller.Save();
        }

        void OnGUI() {
            EditorGUILayout.BeginHorizontal();
            selectedToolTypeIndex = EditorGUILayout.Popup("Tool to add", selectedToolTypeIndex, typesNames);
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_TreeEditor.Refresh"), GUILayout.Width(32))) {
                controller.ReloadTypes();
                _typesNames = controller.editorToolsTypes.Select(t => t.FullName.Replace(".", "/")).ToArray();
            }

            if (GUILayout.Button("+", GUILayout.Width(32))) {
                controller.AddTool(selectedToolType);
            }

            EditorGUILayout.EndHorizontal();
            DrawTools(out List<EditorTool> toRemove);
            
            controller.RemoveTools(toRemove);
        }

        #endregion

        #region Class Implementation

        private void Load() {
            var title = new GUIContent();
            title.text = "Editor tools";
            titleContent = title;

            _controller = new EditorToolsController();
        }

        private void DrawTools(out List<EditorTool> toRemove) {
            toRemove = new List<EditorTool>();
            foreach (var tool in controller.tools) {
                DrawTool(tool, out bool remove);
                if (remove) {
                    toRemove.Add(tool);
                }
            }
        }

        private void DrawTool(EditorTool tool, out bool remove) {
            if (DrawTitle(tool, out remove)) {
                tool.DrawInterface();
            }
        }
        
        private bool DrawTitle(EditorTool tool, out bool remove) {
            remove = false;
            var toolType = tool.GetType();
            var editorToolName = (EditorToolName)Attribute.GetCustomAttribute(toolType, typeof(EditorToolName));
            var toolLabel = editorToolName != null ? editorToolName.toolName : toolType.ToString();
            EditorGUILayout.BeginHorizontal();
            tool.drawToggle = EditorGUILayout.Foldout(tool.drawToggle, toolLabel);
            if (GUILayout.Button("X", GUILayout.Width(32))) {
                remove = true;
            }
            EditorGUILayout.EndHorizontal();
            return tool.drawToggle;
        }

        #endregion

        #region Static Methods

        [MenuItem("Assist/Tools")]
        public static void ShowSettingsWindow() {
            var toolWindow = (EditorToolsWindow)GetWindow(typeof(EditorToolsWindow), true);
            toolWindow.Load();
        }

        #endregion
    }
}
