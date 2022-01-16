using c1tr00z.AssistLib.ResourceManagement.Editor;
using UnityEditor;

namespace c1tr00z.AssistLib.GameUI.Editor {
    public static class EditorUIItemsUtils {

        #region Class Implementation

        [MenuItem("Assets/AssistLib/Create UI Frame DBEntry")]
        public static void CreateUIFrameDBEntry() {
            DBEntryEditorUtils.CreateItem<UIFrameDBEntry>();
        }

        #endregion
    }
}
