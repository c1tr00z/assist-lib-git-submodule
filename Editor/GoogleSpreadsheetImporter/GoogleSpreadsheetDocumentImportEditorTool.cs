using System.Collections.Generic;
using c1tr00z.AssistLib.Json;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.GoogleSpreadsheetImporter {
    public abstract class GoogleSpreadsheetDocumentImportEditorTool : EditorTools.EditorTool {

        #region Private Fields

        private static string PAGES_KEY = "pages";

        #endregion

        #region JSON Fields

        [JsonSerializableField]
        public List<GoogleSpreadsheetDocumentPageDBEntry> pages = new List<GoogleSpreadsheetDocumentPageDBEntry>();

        #endregion

        #region EditorTool Implementation

        public override void DrawInterface() {

            EditorGUILayout.BeginVertical();

            if (Button("+")) {
                pages.Add(null);
            }

            for (var i = 0; i < pages.Count; i++) {
                EditorGUILayout.BeginHorizontal();
                pages[i] = (GoogleSpreadsheetDocumentPageDBEntry)EditorGUILayout.ObjectField(pages[i], typeof(GoogleSpreadsheetDocumentPageDBEntry), false);
                if (Button("-", GUILayout.Width(50))) {
                    RemoveIndex(i);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();

            if (Button("Import")) {
                ProcessImport();
            }

            EditorGUILayout.EndHorizontal();
        }
        
        #endregion

        #region Class Implementation

        private void RemoveIndex(int index) {
            pages.RemoveAt(index);
        }

        #endregion

        #region Abstract Methods

        protected abstract void ProcessImport();

        #endregion
        
    }
}