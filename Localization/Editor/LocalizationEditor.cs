using c1tr00z.AssistLib.ResourceManagement.Editor;
using c1tr00z.AssistLib.GoogleSpreadsheetImporter;
using UnityEditor;

namespace c1tr00z.AssistLib.Localization.Editor {
    public static class LocalizationEditor {

        #region Class Implementation

        [MenuItem("Assets/AssistLib/Create Google Spreadsheet Document")]
        public static void CreateSpreadsheetDocument() {
            DBEntryEditorUtils.CreateItem<GoogleSpreadsheetDocumentDBEntry>();
        }

        [MenuItem("Assets/AssistLib/Create Google Spreadsheet Document Page")]
        public static void CreateSpreadsheetDocumentPage() {
            DBEntryEditorUtils.CreateItem<GoogleSpreadsheetDocumentPageDBEntry>();
        }

        #endregion
    }
}
