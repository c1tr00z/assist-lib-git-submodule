using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace AssistLib.Editor.DB {
    [CustomEditor(typeof(DBEntry), true)]
    public class DBEntryGenericInspector : UnityEditor.Editor {

        #region Editor Implementation

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            PingObject("Prefab");
        }

        #endregion

        #region Class Implementation

        protected void PingObject(string key) {
            if (GUILayout.Button($"Ping {key}")) {
                EditorGUIUtils.PingObjectByName($"{target.name}@{key}");
            }
        }

        #endregion
    }
}