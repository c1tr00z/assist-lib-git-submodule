using UnityEditor;

namespace c1tr00z.AssistLib.ResourceManagement.Editor {
    [InitializeOnLoad]
    public class DBEntriesEditorEvents {
        public DBEntriesEditorEvents() {
            EditorApplication.projectChanged += EditorApplicationOnProjectChanged;
            DBEntryEditorUtils.CollectItems();
        }

        private void EditorApplicationOnProjectChanged() {
            DBEntryEditorUtils.AutoCollect();
        }
    }
}