using UnityEditor;

namespace c1tr00z.AssistLib.ResourcesManagement.Editor {
    [InitializeOnLoad]
    public class DBEntriesEditorEvents {
        public DBEntriesEditorEvents() {
            EditorApplication.projectChanged += EditorApplicationOnProjectChanged;
            DBEntryEditorActions.CollectItems();
        }

        private void EditorApplicationOnProjectChanged() {
            // DBEntryEditorActions.AutoCollect();
        }
    }
}