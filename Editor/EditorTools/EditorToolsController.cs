using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using c1tr00z.AssistLib.Editor;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    public class EditorToolsController {

        #region Private Fields

        private static string _editorToolsSettingsKey = "EditorTools";

        private Dictionary<Type, EditorTool> tools = new Dictionary<Type, EditorTool>();

        #endregion

        #region Constructirs

        public EditorToolsController() {
            var settingsHash = AssistLibEditorSettings.GetDataNode(_editorToolsSettingsKey);

            AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(assembly => {
                assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(EditorTool)) && !t.IsAbstract).ToList().ForEach(t => {
                    if (!tools.ContainsKey(t)) {
                        var tool = (EditorTool)Activator.CreateInstance(t);
                        tools.Add(t, tool);
                        tool.Init(settingsHash.GetChild(t.ToString()));
                    }
                });
            });
        }

        #endregion

        #region Class Implementation

        public void DrawTools() {
            tools.Values.ToList().ForEach(tool => tool.Draw());
        }

        public void SaveTools() {
            var settingsHash = AssistLibEditorSettings.GetDataNode(_editorToolsSettingsKey);
            tools.Values.ToList().ForEach(tool => {
                var toolSettings = settingsHash.GetChild(tool.GetType().ToString());
                tool.Save(toolSettings);
                settingsHash.AddOrSet(tool.GetType().ToString(), toolSettings);
            });
            AssistLibEditorSettings.SetDataNode(_editorToolsSettingsKey, settingsHash);
        }

        #endregion
    }
}
