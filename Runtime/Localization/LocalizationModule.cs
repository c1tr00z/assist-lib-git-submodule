using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.Json;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Localization {
    public class LocalizationModule : Module {

        #region Events

        public static event Action<LanguageItem> LanguageChanged; 

        #endregion

        #region Nested Classes

        private class Settings : IJsonSerializable, IJsonDeserializable {

            [JsonSerializableField] public string savedLanguage;
        }

        #endregion

        #region Readonly Fields

        private static readonly string LOCALIZATION_SETTINGS_KEY = "Localization";
        private static readonly string LOCALIZATION_SAVED_LANGUAGE_KEY = "Localization";

        #endregion

        #region Const Fields

        private const SystemLanguage _defaultSystemLanguage = SystemLanguage.English;

        #endregion

        #region Private Fields

        private Dictionary<LanguageItem, Dictionary<string, string>> _translations =
            new Dictionary<LanguageItem, Dictionary<string, string>>();

        private SystemLanguage _currentSystemLanguage;
        
        private LanguageItem _defaultLanguage;

        private LocalizationSettingsDBEntry _settingsDBEntry;

        private Settings _settings;

        #endregion

        #region Accessors

        private LocalizationSettingsDBEntry settingsDBEntry => DBEntryUtils.GetCached(ref _settingsDBEntry);

        public LanguageItem currentLanguage { get; private set; }

        private bool isMultipleTranslationsSupported => settingsDBEntry.supportMultipleTranslations;

        #endregion

        #region Unity Events

        private void Awake() {
            _currentSystemLanguage = Application.systemLanguage;
        }

        #endregion

        #region Module Implementation

        public override void InitializeModule(CoroutineRequest request) {
            StartCoroutine(C_InitializeModule(request));
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_InitializeModule(CoroutineRequest request) {

            var allLanguageItems = DB.GetAll<LanguageItem>();

            foreach (var languageItem in allLanguageItems) {
                var textRequest = languageItem.LoadTextAsync();
                yield return textRequest;
                var textAsset = textRequest.asset;

                if (textAsset == null) {
                    continue;
                }
                
                var langHash = JSONUtils.Deserialize(textRequest.asset.text);
                _translations.Add(languageItem, langHash.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString()));
            }
            
            _settingsDBEntry = DB.Get<LocalizationSettingsDBEntry>();
                
            _defaultLanguage = DB.Get<LanguageItem>(_defaultSystemLanguage.ToString());

            var localizationSettingsData = PlayerPrefsLocalData.GetDataNode(LOCALIZATION_SETTINGS_KEY);

            _settings = JSONUtils.Deserialize<Settings>(localizationSettingsData);

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
            
            base.InitializeModule(request);
        }

        public string GetTranslationString(string key) {
            if (!isInitialized) {
                return key;
            }
            
            var translation = key;

            if (_defaultLanguage != null &&
                _translations.TryGetValue(_defaultLanguage, out Dictionary<string, string> defaultTranslations) &&
                defaultTranslations.ContainsKey(key) && !string.IsNullOrEmpty(defaultTranslations[key])) {

                translation = defaultTranslations[key];
            }
            
            
            if (currentLanguage != null &&
                _translations.TryGetValue(currentLanguage, out Dictionary<string, string> translations) &&
                translations.ContainsKey(key) && !string.IsNullOrEmpty(translations[key])) {
            
                translation = translations[key];
            }

            return translation;
        }
        
        public void ChangeLanguage(string newLanguageName) {
            var newLanguage = DB.Get<LanguageItem>(newLanguageName);

            if (newLanguage == null) {
                Debug.LogError("Invalid language: " + newLanguageName);
                return;
            }

            ChangeLanguage(newLanguage);
        }

        public void ChangeLanguage(LanguageItem newLanguage) {
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

            if (!isInitialized) {
                return;
            }

            LanguageChanged?.Invoke(newLanguage);
        }
        
        public string Translate(string key) {

            var translation = GetTranslationString(key);

            if (isMultipleTranslationsSupported) {
                translation = translation.Split('|').First();
            }

            return translation;
        }
        
        public string TranslateRandom(string key) {

            var translation = GetTranslationString(key);

            if (isMultipleTranslationsSupported) {
                translation = translation.Split('|').RandomItem();
            }

            return translation;
        }

        
        #endregion
    }
}