using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    public class EditorToolsWindow : EditorWindow {

        #region Private Fields

        private EditorToolsController _controller;

        #endregion

        #region Unity Events

        void OnGUI() {

            if (_controller == null) {
                Load();
                return;
            }

            _controller.DrawTools();

            if (GUI.changed) {
                _controller.SaveTools();
            }
        }

        #endregion

        #region Class Implementation

        private void Load() {
            var title = new GUIContent();
            title.text = "Editor tools";
            titleContent = title;

            _controller = new EditorToolsController();
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
