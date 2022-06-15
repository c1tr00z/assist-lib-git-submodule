using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.EditorTools;
using c1tr00z.AssistLib.Json;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement.Editor {
    [EditorToolName("DB Settings")]
    public class DBSettings : EditorTool {

        #region Public Fields

        [JsonSerializableField]
        public bool autoCollect;

        #endregion

        #region Class Implementation

        public override void DrawInterface() {
            autoCollect = EditorGUILayout.Toggle("Use autocollect", autoCollect);
        }

        #endregion
    }
}