using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

public class PlayerPrefsLocalData : MonoBehaviour {

    #region Private Fields

    private static Dictionary<string, object> _playerPrefsLocalData = new Dictionary<string, object>();

    #endregion

    #region Accessors

    public static string editorSettingsKey => "AssistLib";

    #endregion

    #region Class Implementation

    private static void CheckLoading() {
        if (_playerPrefsLocalData == null) {
            var settingsJson = PlayerPrefs.GetString(editorSettingsKey);
            if (string.IsNullOrEmpty(settingsJson)) {
                _playerPrefsLocalData = new Dictionary<string, object>();
            } else {
                _playerPrefsLocalData = JSONUtils.Deserialize(settingsJson);
            }
        }
    }

    public static Dictionary<string, object> GetDataNode(string key) {
        CheckLoading();
        return _playerPrefsLocalData.ContainsKey(key) 
            ? _playerPrefsLocalData[key] is string 
                ? JSONUtils.Deserialize(_playerPrefsLocalData[key].ToString()) 
                : (Dictionary<string, object>)_playerPrefsLocalData[key] 
            : new Dictionary<string, object>();
    }

    public static void SetDataNode(string key, Dictionary<string, object> node) {
        _playerPrefsLocalData.AddOrSet(key, node);
        Save();
    }

    public static void Save() {
        PlayerPrefs.SetString(editorSettingsKey, JSONUtils.Serialize(_playerPrefsLocalData));
    }

    #endregion
}
