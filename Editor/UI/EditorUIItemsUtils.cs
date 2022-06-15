using c1tr00z.AssistLib.ResourcesManagement.Editor;
using UnityEditor;

namespace c1tr00z.AssistLib.GameUI.Editor {
    public static class EditorUIItemsUtils {

        #region Class Implementation

        [MenuItem("Assets/AssistLib/Create UI Frame DBEntry")]
        public static void CreateUIFrameDBEntry() {
            DBEntryEditorActions.CreateItem<UIFrameDBEntry>();
        }

        #endregion
    }
}
