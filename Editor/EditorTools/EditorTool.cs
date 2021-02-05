using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Json;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    public class EditorTool : IJsonSerializableCustom, IJsonDeserializableCustom {

        #region Private Fields

        private string _toolLabel = "Empty...";

        #endregion

        #region JSON Fields

        [JsonSerializableField] public bool drawToggle = true;

        #endregion

        #region IJsonSerializableCustom Implementation

        public virtual void SerializeCustom(Dictionary<string, object> json) {
            json.Add("editorToolType", GetType().FullName);
        }

        public virtual void DeserializeCustom(Dictionary<string, object> json) {
            
        }

        #endregion

        #region Class Implementation

        public virtual void DrawInterface() {
            EditorGUILayout.LabelField("Nothing here...");
        }

        protected bool Button(string caption, params GUILayoutOption[] options) {
            return GUILayout.Button(caption, options);
        }

        protected void Label(string caption) {
            EditorGUILayout.LabelField(caption);
        }

        #endregion
    }
}
