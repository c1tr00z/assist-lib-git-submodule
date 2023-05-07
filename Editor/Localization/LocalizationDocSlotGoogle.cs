using System.Collections;
using System.Collections.Generic;
using AssistLib.Editor.Utils;
using c1tr00z.AssistLib.Json;
using c1tr00z.AssistLib.Localization.GoogleSpreadsheetImporter;
using c1tr00z.AssistLib.Utils;
using UnityEditor;

namespace AssistLib.Editor.Localization {
    public class LocalizationDocSlotGoogle : LocalizationDocSlot, IJsonSerializableCustom, IJsonDeserializableCustom {

        #region Private Fields

        private EditorGUIListData<long> _pagesData = new("pages ids");

        #endregion
        
        #region Json Fields

        [JsonSerializableField] public string docId;

        #endregion

        #region LocalizationDocSlot Implementation

        public override void DrawSlotGUI() {
            docId = EditorGUILayout.TextField("Document Id", docId);
            EditorGUIUtils.ListField(_pagesData);
        }

        public override Dictionary<string, Dictionary<string, string>> Import() {
            var allGoogleLocalizations = new Dictionary<string, Dictionary<string, string>>();
            _pagesData.items.ForEach(pageId => {
                var pageLocalization = GoogleSpreadsheetDocumentImpoter.Import(docId, pageId);
                foreach (var line in pageLocalization) {
                    allGoogleLocalizations.Add(line.Key, line.Value);
                }
            });
            return allGoogleLocalizations;
        }

        #endregion

        #region JsonSerializableCustom Implementation

        public void SerializeCustom(Dictionary<string, object> json) {
            json.AddOrSet("pages", _pagesData.items);
        }

        #endregion

        #region IJsonDeserializableCustom Implementation

        public void DeserializeCustom(Dictionary<string, object> json) {
            _pagesData = new EditorGUIListData<long>();
            if (!json.TryGetValue("pages", out object valuesObj)) {
                return;
            }

            if (valuesObj is IList list) {
                foreach (var o in list) {
                    if (o is long longVal) {
                        _pagesData.items.Add(longVal);
                    }
                }
            }
        }

        #endregion
    }
}