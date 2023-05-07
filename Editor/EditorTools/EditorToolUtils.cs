using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    public static class EditorToolUtils {

        #region Private Fields
        
        private static GUIStyle _editorToolStyle;

        #endregion

        #region Accessors

        public static GUIStyle editorToolStyle {
            get {
                if (_editorToolStyle == null) {
                    _editorToolStyle = new GUIStyle(EditorStyles.helpBox) { contentOffset = new Vector2(16, 0) };
                }

                return _editorToolStyle;
            }
        }

        #endregion
    }
}