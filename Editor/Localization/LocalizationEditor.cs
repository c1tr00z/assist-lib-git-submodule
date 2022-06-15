using c1tr00z.AssistLib.GoogleSpreadsheetImporter;
using c1tr00z.AssistLib.ResourcesManagement.Editor;
using UnityEditor;

namespace c1tr00z.AssistLib.Localization.Editor {
    public static class LocalizationEditor {

        #region Class Implementation

        [MenuItem("Assets/AssistLib/Create Google Spreadsheet Document")]
        public static void CreateSpreadsheetDocument() {
            DBEntryEditorActions.CreateItem<GoogleSpreadsheetDocumentDBEntry>();
        }

        [MenuItem("Assets/AssistLib/Create Google Spreadsheet Document Page")]
        public static void CreateSpreadsheetDocumentPage() {
            DBEntryEditorActions.CreateItem<GoogleSpreadsheetDocumentPageDBEntry>();
        }

        #endregion
    }
}
