using c1tr00z.AssistLib.GoogleSpreadsheetImporter;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.EditorTools;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Localization {
    [EditorToolName("Localization tool")]
    public class LocalizationEditorTool : GoogleSpreadsheetDocumentImportEditorTool {

        #region GoogleSpreadsheetDocumentImportEditorTool Implementation

        protected override void ProcessImport() {
            var allLocalizations = new Dictionary<string, Dictionary<string, string>>();
            pages.ForEach(p => {
                var pageLocalizations = GoogleSpreadsheetDocumentImpoter.Import(p);
                pageLocalizations.Keys.ToList().ForEach(language => {
                    if (allLocalizations.ContainsKey(language)) {
                        pageLocalizations[language].Keys.ToList().ForEach(key => allLocalizations[language].AddOrSet(key, pageLocalizations[language][key]));
                    } else {
                        allLocalizations.AddOrSet(language, pageLocalizations[language]);
                    }
                });
            });
            StoreLocalization(allLocalizations);
        }

        public override void DrawInterface() {
            base.DrawInterface();
            if (Button("Print localization fields")) {
                PrintLocalizationFields();
            }
        }

        #endregion

        #region Class Implementation

        private void StoreLocalization(Dictionary<string, Dictionary<string, string>> localization) {
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in localization) {
                var lang = DB.Get<LanguageItem>(kvp.Key);
                if (lang == null) {
                    lang = ScriptableObjectsEditorUtils.Create<LanguageItem>(PathUtils.Combine("Assets", "Localization", "Resources", "Languages"), kvp.Key);
                }
                var json = JSONUtils.Serialize(kvp.Value).DecodeEncodedNonAscii();
                FileUtils.SaveTextToFile(PathUtils.Combine(Application.dataPath, "Localization", "Resources", "Languages", kvp.Key + "@text.txt"), json);
                AssetDatabase.Refresh();
            }

            Debug.Log(JSONUtils.Serialize(localization).DecodeEncodedNonAscii());
        }

        private void PrintLocalizationFields() {
            Debug.Log(GetFields().Select(f => f.GetLocalizationKey()).ToPlainString("\r\n"));
        }

        private List<FieldInfo> GetFields() {
            var fields = new List<FieldInfo>();
            ReflectionUtils.GetTypesList().Select(t => t.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(f => f.GetLocalizationAttribute() != null))
                .ToList().ForEach(f => fields.AddRange(f));
            return fields;
        }

        #endregion
    }
}