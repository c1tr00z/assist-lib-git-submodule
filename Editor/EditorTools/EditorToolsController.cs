using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Editor;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    public class EditorToolsController {

        #region Private Fields

        private static string EDITOR_TOOLS_SETTINGS_KEY = "EditorTools";

        private List<EditorTool> _tools = new List<EditorTool>();

        private EditorToolsSaveData _data;
        
        private List<Type> _toolsTypes = new List<Type>();

        #endregion

        #region Accessors

        private EditorToolsSaveData data {
            get {
                if (_data == null) {
                    Load();
                }

                return _data;
            }
        }

        public List<Type> editorToolsTypes {
            get {
                if (_toolsTypes.Count == 0) {
                    AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(assembly => {
                        assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(EditorTool)) && !t.IsAbstract).ToList().ForEach(t => {
                            if (!_toolsTypes.Contains(t)) {
                                _toolsTypes.Add(t);
                            }
                        });
                    });
                }

                return _toolsTypes;
            }
        }

        public List<EditorTool> tools => _tools;

        #endregion

        #region Constructirs

        public EditorToolsController() {
            if (editorToolsTypes.Count > 0) {
                data.toolsData.ForEach(toolData => {
                    var type = editorToolsTypes.FirstOrDefault(t => t.FullName == toolData.typeName);
                    if (type == null) {
                        return;
                    }

                    var tool = (EditorTool) Activator.CreateInstance(type);
                    tool.Deserialize(toolData.data);
                    _tools.Add(tool);
                });
            }
        }

        #endregion

        #region Class Implementation

        public void Load() {
            _data = AssistLibEditorSettings.Get<EditorToolsSaveData>(EDITOR_TOOLS_SETTINGS_KEY);
            if (_data == null) {
                _data = new EditorToolsSaveData();
            }
        }

        public void Save() {
            data.toolsData = tools.Select(t => {
                var toolData = new EditorToolsSaveData.EditorToolData();
                toolData.typeName = t.GetType().FullName;
                t.Serialize(toolData.data);
                return toolData;
            }).ToList();
            AssistLibEditorSettings.Set(EDITOR_TOOLS_SETTINGS_KEY, data);
        }

        public void ReloadTypes() {
            _toolsTypes.Clear();
            AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(assembly => {
                assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(EditorTool)) && !t.IsAbstract).ToList().ForEach(t => {
                    if (!_toolsTypes.Contains(t)) {
                        _toolsTypes.Add(t);
                    }
                });
            });
        }

        public void AddTool(Type type) {
            if (type == null || !type.IsSubclassOf(typeof(EditorTool))) {
                return;
            }
            tools.Add((EditorTool)Activator.CreateInstance(type));
            Save();
        }

        public void RemoveTool(EditorTool tool) {
            if (tool == null || !tools.Contains(tool)) {
                return;
            }

            tools.Remove(tool);
        }

        public void RemoveTools(List<EditorTool> toolsToRemove) {
            if (toolsToRemove == null || toolsToRemove.Count == 0) {
                return;
            }

            toolsToRemove.ForEach(RemoveTool);
        }

        #endregion
    }
}
