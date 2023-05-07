using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AssistLib.Editor.Localization;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.EditorTools;
using c1tr00z.AssistLib.Json;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.ResourcesManagement.Editor;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Localization {
    [EditorToolName("Localization tool")]
    public class LocalizationEditorTool : EditorTool {

        #region Json Fields

        [JsonSerializableField] public List<LocalizationDocSlotGoogle> googleSlots = new();

        #endregion

        #region EditorTool Implementation

        public override void DrawInterface() {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Slots");
            if (EditorGUIUtils.PlusButton()) {
                googleSlots.Add(new LocalizationDocSlotGoogle());
            }
            EditorGUILayout.EndHorizontal();
            var toRemove = new List<LocalizationDocSlotGoogle>();
            for (var i = 0; i < googleSlots.Count; i++) {
                EditorGUILayout.BeginVertical(EditorToolUtils.editorToolStyle);
                EditorGUIUtils.BeginIndentedBox();
                EditorGUILayout.BeginHorizontal();
                var slot = googleSlots[i];
                GUILayout.Label($"Slot #{i}");
                if (EditorGUIUtils.RemoveButton()) {
                    toRemove.Add(slot);
                }
                EditorGUILayout.EndHorizontal();
                slot.DrawSlotGUI();
                EditorGUIUtils.EndIndentedBox();
                EditorGUILayout.EndVertical();
            }
            toRemove.ForEach(s => googleSlots.Remove(s));

            if (Button("Import")) {
                ProcessImport();
            }

            if (Button("Check directories")) {
                CheckDirectories();
            }
        }

        #endregion

        #region Class Implementation

        private void ProcessImport() {
            var allLocalizations = new Dictionary<string, Dictionary<string, string>>();
            googleSlots.ForEach(slot => {
                var pageLocalizations = slot.Import();
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

        private void StoreLocalization(Dictionary<string, Dictionary<string, string>> localization) {
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in localization) {
                var lang = DB.Get<LanguageItem>(kvp.Key);
                if (lang == null) {
                    lang = ScriptableObjectsEditorUtils.Create<LanguageItem>(PathUtils.Combine("Assets", "Localization", "Resources", "Languages"), kvp.Key);
                    DBEntryEditorActions.CollectItems();
                }
                var json = JSONUtils.Serialize(kvp.Value).DecodeEncodedNonAscii();
                FileUtils.SaveTextToFile(PathUtils.Combine(Application.dataPath, "Localization", "Texts", "Languages", kvp.Key + "@Text.txt"), json);
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

        private void CheckDirectories() { 
            var resourcePath = PathUtils.Combine(Application.dataPath, "Localization", "Resources", "Languages");
            if (!Directory.Exists(resourcePath)) {
                Directory.CreateDirectory(resourcePath);
            }
            var assetsPath = PathUtils.Combine(Application.dataPath, "Localization", "Texts", "Languages");
            if (!Directory.Exists(assetsPath)) {
                Directory.CreateDirectory(assetsPath);
            }
            AssetDatabase.Refresh();
        }

        #endregion
    }
}