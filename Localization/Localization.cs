using UnityEngine;
using c1tr00z.AssistLib.Json;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.Localization {
    public static class Localization {

        private class Settings : IJsonSerializable, IJsonDeserializable {

            [JsonSerializableField] public string savedLanguage;
        }

        private const SystemLanguage _defaultSystemLanguage = SystemLanguage.English;
        private static SystemLanguage _currentSystemLanguage = Application.systemLanguage;
        private static LanguageItem _defaultLanguage;
        public static LanguageItem currentLanguage { get; private set; }

        private static string LOCALIZATION_SETTINGS_KEY = "Localization";
        private static string LOCALIZATION_SAVED_LANGUAGE_KEY = "Localization";

        private static LocalizationSettingsDBEntry _settingsDBEntry;

        private static Settings _settings;

        private static bool _inited = false;

        public static System.Action<LanguageItem> changeLanguage = (language) => { };

        public static bool isMultipleTranslationsSupported {
            get {
                Init();
                return _settingsDBEntry != null && _settingsDBEntry.supportMultipleTranslations;
            }
        }

        private static void Init() {
            if (!_inited) {
                _settingsDBEntry = DB.Get<LocalizationSettingsDBEntry>();
                
                _defaultLanguage = DB.Get<LanguageItem>(_defaultSystemLanguage.ToString());

                var localizationSettingsData = PlayerPrefsLocalData.GetDataNode(LOCALIZATION_SETTINGS_KEY);

                _settings = JSONUtuls.Deserialize<Settings>(localizationSettingsData);

                if (_settings == null) {
                    _settings = new Settings();
                }

                if (string.IsNullOrEmpty(_settings.savedLanguage)) {
                    ChangeLanguage(_currentSystemLanguage.ToString());
                } else {
                    var savedLanguage = DB.Get<LanguageItem>(_settings.savedLanguage);
                    if (savedLanguage != null) {
                        ChangeLanguage(savedLanguage);
                    } else {
                        Debug.LogWarning(string.Format("Language not found: {0}", _settings.savedLanguage));
                    }
                }

                _inited = true;
            }
        }

        private static string GetTranslationString(string key) {
            var translation = key;

            Init();

            if (_defaultLanguage != null && _defaultLanguage.translations != null 
                && _defaultLanguage.translations.ContainsKey(key) && !string.IsNullOrEmpty(_defaultLanguage.translations[key])) {
                translation = _defaultLanguage.translations[key];
            }

            if (currentLanguage != null && currentLanguage.translations != null 
                && currentLanguage.translations.ContainsKey(key) && !string.IsNullOrEmpty(currentLanguage.translations[key])) {
                translation = currentLanguage.translations[key];
            }

            return translation;
        }

        public static string Translate(string key) {

            var translation = GetTranslationString(key);

            if (isMultipleTranslationsSupported) {
                translation = translation.Split('|').First();
            }

            return translation;
        }
        
        public static string TranslateRandom(string key) {

            var translation = GetTranslationString(key);

            if (isMultipleTranslationsSupported) {
                translation = translation.Split('|').RandomItem();
            }

            return translation;
        }

        public static void ChangeLanguage(string newLanguageName) {
            var newLanguage = DB.Get<LanguageItem>(newLanguageName);

            if (newLanguage == null) {
                Debug.LogError("Invalid language: " + newLanguageName);
                return;
            }

            ChangeLanguage(newLanguage);
        }

        public static void ChangeLanguage(LanguageItem newLanguage) {
            if (currentLanguage == newLanguage) {
                return;
            }

            if (_settings == null) {
                _settings = new Settings();
            }

            _settings.savedLanguage = newLanguage.name;

            var localizationSettingsData = _settings.ToJson();
            
            PlayerPrefsLocalData.SetDataNode(LOCALIZATION_SETTINGS_KEY, localizationSettingsData);
            currentLanguage = newLanguage;

            if (_inited) {
                changeLanguage(newLanguage);
            }
        }
    }
}
