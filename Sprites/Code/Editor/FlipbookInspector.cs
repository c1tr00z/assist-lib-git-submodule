using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Sprites.Editor {
    // [CustomEditor(typeof(Flipbook))]
    public class FlipbookInspector : UnityEditor.Editor {

        #region Accessors

        private Flipbook flipbook => target as Flipbook;

        #endregion

        #region Editor Implementation

        // public override void OnInspectorGUI() {
        //     base.OnInspectorGUI();
        // }

        // public override bool HasPreviewGUI() {
        //     return true;
        // }

        // public override void OnPreviewGUI(Rect r, GUIStyle background) {
        //     base.OnPreviewGUI(r, background);
        //     // EditorGUI.DrawPreviewTexture(r, flipbook[0].texture, null, ScaleMode.StretchToFill);
        //     // EditorGUI.DrawPreviewTexture(r, flipbook.tex);
        // }

        #endregion

    }
}