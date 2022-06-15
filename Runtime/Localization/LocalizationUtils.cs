using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.Localization {
    public static class LocalizationUtils {

        #region Readonly Fields

        private static readonly string KEY_TITLE = "Title";
        private static readonly string KEY_DESCRIPTION = "Description";

        #endregion

        #region Private Fields

        private static LocalizationModule _module;

        #endregion

        #region Accessors

        private static LocalizationModule module => ModulesUtils.GetCachedModule(ref _module);

        #endregion

        #region Class Implementation

        public static string GetLocalizationText(this string key) {
            return module.Translate(key);
        }
        
        public static string GetLocalizationTextRandom(this string key) {
            return module.TranslateRandom(key);
        }
        
        public static string GetLocalizationTextRandom(this string key, params object[] localizationParams) {
            return string.Format(GetLocalizationTextRandom(key), localizationParams);
        }
        
        public static string GetLocalizationText(this string key, params object[] localizationParams) {
            return string.Format(GetLocalizationText(key), localizationParams);
        }

        public static string GetLocalizationTextRandom(this DBEntry dBEntry, string key) {
            return $"{dBEntry.name}@{key}".GetLocalizationTextRandom();
        }
        
        public static string GetLocalizationText(this DBEntry dBEntry, string key) {
            return $"{dBEntry.name}@{key}".GetLocalizationText();
        }

        public static string GetLocalizationTextRandom(this DBEntry dBEntry, string key, params object[] localizationParams) {
            return string.Format(dBEntry.GetLocalizationTextRandom(key), localizationParams);
        }
        
        public static string GetLocalizationText(this DBEntry dBEntry, string key, params object[] localizationParams) {
            return string.Format(dBEntry.GetLocalizationText(key), localizationParams);
        }

        public static string GetTitle(this DBEntry dBEntry) {
            return GetLocalizationText(dBEntry, KEY_TITLE);
        }

        public static string GetDescription(this DBEntry dBEntry) {
            return GetLocalizationText(dBEntry, KEY_DESCRIPTION);
        }

        public static string Translate(string key) {
            return module.Translate(key);
        }

        public static string TranslateRandom(string key) {
            return module.TranslateRandom(key);
        }

        #endregion
    }
}