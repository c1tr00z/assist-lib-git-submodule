using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using UnityEditor;

namespace c1tr00z.AssistLib.Editor {
    public class AssistLibEditorSettings {

        #region Private Fields

        private static Dictionary<string, object> _editorSettingsData = new Dictionary<string, object>();

        #endregion

        #region Accesors

        public static string editorSettingsKey => "AssistLibEditorSettings";

        #endregion

        #region Class Implementation

        private static void CheckLoading() {
            if (_editorSettingsData == null || _editorSettingsData.Count == 0) {
                var settingsJson = EditorPrefs.GetString(editorSettingsKey);
                if (string.IsNullOrEmpty(settingsJson)) {
                    _editorSettingsData = new Dictionary<string, object>();
                } else {
                    var deserialized = JSONUtuls.Deserialize(settingsJson);
                    if (deserialized != null) {
                        _editorSettingsData.AddOrSetRange(deserialized);
                    }
                }
            }
        }

        public static Dictionary<string, object> GetDataNode(string key) {
            CheckLoading();
            return _editorSettingsData.ContainsKey(key) ? (Dictionary<string, object>)_editorSettingsData[key] : new Dictionary<string, object>();
        }

        public static void SetDataNode(string key, Dictionary<string, object> node) {
            _editorSettingsData.AddOrSet(key, node);
            Save();
        }

        public static void Save() {
            EditorPrefs.SetString(editorSettingsKey, JSONUtuls.Serialize(_editorSettingsData));
        }

        #endregion
    }
}

