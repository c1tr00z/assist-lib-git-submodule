using UnityEditor;
using UnityEngine;

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

        #endregion

    }
}
