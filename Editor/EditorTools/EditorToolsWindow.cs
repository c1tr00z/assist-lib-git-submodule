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

        private static EditorToolsController _controller;

        private int _selectedToolTypeIndex = 0;

        private string[] _typesNames = new string [0];

        #endregion

        #region Accessors

        private static EditorToolsController controller {
            get {
                if (_controller == null) {
                    _controller = new EditorToolsController();
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

        private bool isUtility {
            get => controller.isUtilityWindow;
            set {
                if (controller.isUtilityWindow == value) {
                    return;
                }

                controller.isUtilityWindow = value;
                Close();
                ShowToolsWindows();
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
            isUtility = EditorGUILayout.Toggle("Is utility window", isUtility);
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
        }

        private void DrawTools(out List<EditorTool> toRemove) {
            EditorGUI.indentLevel++;
            toRemove = new List<EditorTool>();
            foreach (var tool in controller.tools) {
                // DrawTool(tool, out bool remove);
                DrawToolGUI(tool, out bool remove);
                if (remove) {
                    toRemove.Add(tool);
                }
            }
            EditorGUI.indentLevel--;
        }
        
        private void DrawToolGUI(EditorTool tool, out bool markedToRemove) {
            markedToRemove = false;
            GUILayout.Space(EditorGUI.indentLevel);
            EditorGUILayout.BeginVertical(EditorToolUtils.editorToolStyle);
            EditorGUILayout.BeginHorizontal();
            var isShow = EditorGUILayout.BeginFoldoutHeaderGroup(tool.drawToggle, tool.title);
            if (isShow != tool.drawToggle) {
                tool.drawToggle = isShow;
                if (tool.drawToggle) {
                    // tool.OnToolEnable();
                } else {
                    // tool.OnToolDisable();
                }
            }
            if (EditorGUIUtils.ConsoleButton()) {
                EditorToolsController.OpenToolCode(tool);
            }
            if (GUILayout.Button("X", GUILayout.Width(24))) {
                markedToRemove = true;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUIUtils.BeginIndentedBox();
            if (tool.drawToggle) {
                tool.DrawInterface();
            }
            EditorGUIUtils.EndIndentedBox();
            EditorGUILayout.EndVertical();
            
            //Hotfix
            EditorGUIUtils.BalanceIndentLevel();
            GUI.enabled = true;
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
        public static void ShowToolsWindows() {
            var toolWindow = (EditorToolsWindow)GetWindow(typeof(EditorToolsWindow), controller.isUtilityWindow);
            toolWindow.Load();
        }

        #endregion
    }
}
