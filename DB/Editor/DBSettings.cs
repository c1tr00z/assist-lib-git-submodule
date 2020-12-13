using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.EditorTools;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourceManagement.Editor {
    [EditorToolName("DB Settings")]
    public class DBSettings : EditorTool {

        #region Nested Classes

        [Serializable]
        public class SaveData {
            public bool autoCollect;
        }

        #endregion

        #region Private Fields

        private SaveData _saveData;

        #endregion

        #region Accessors

        private static string key => typeof(DBSettings).FullName;

        public static bool autoCollect => LoadData().autoCollect;

        #endregion

        #region Class Implementation

        public override void Init(Dictionary<string, object> settingsJson) {
            base.Init(settingsJson);
            Load();
        }

        public override void Save(Dictionary<string, object> settingsJson) {
            base.Save(settingsJson);
            Save();
        }

        protected override void DrawInterface() {
            _saveData.autoCollect = EditorGUILayout.Toggle("Use autocollect", _saveData.autoCollect);
        }

        private static SaveData LoadData() {
            var json = EditorPrefs.GetString(key);
            return string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
        }

        private void Load() {
            _saveData = LoadData();
        }

        private void Save() {
            var json = JsonUtility.ToJson(_saveData);
            EditorPrefs.SetString(key, json);
        }

        #endregion
    }
}