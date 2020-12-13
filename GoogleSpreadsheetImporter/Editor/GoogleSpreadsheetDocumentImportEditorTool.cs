using System.Collections.Generic;
using c1tr00z.AssistLib.Json;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.GoogleSpreadsheetImporter {
    public abstract class GoogleSpreadsheetDocumentImportEditorTool : EditorTools.EditorTool {

        #region Nested Classes

        public class Settings : IJsonSerializable, IJsonDeserializable {

            [JsonSerializableField]
            public List<GoogleSpreadsheetDocumentPageDBEntry> pages = new List<GoogleSpreadsheetDocumentPageDBEntry>();
        }

        #endregion
        
        #region Private Fields

        private static string PAGES_KEY = "pages";

        #endregion

        #region Accessors

        protected Settings settings { get; private set; }

        protected List<GoogleSpreadsheetDocumentPageDBEntry> pages => settings.pages;

        #endregion

        #region EditorTool Implementation

        public override void Init(Dictionary<string, object> settingsJson) {
            settings = JSONUtuls.Deserialize<Settings>(settingsJson);
        }

        public override void Save(Dictionary<string, object> settingsJson) {
            base.Save(settingsJson);
            settings.Serialize(settingsJson);
            // settingsJson.AddOrSet(PAGES_KEY, pages.SelectNotNull().SelectNotNull(p => p.name).ToArray());
        }
        
        protected override void DrawInterface() {

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