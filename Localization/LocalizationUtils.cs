﻿using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.Localization {
    public static class LocalizatioUtils {

        #region Readonly Fields

        private static readonly string KEY_TITLE = "Title";
        private static readonly string KEY_DESCRIPTION = "Description";

        #endregion

        #region Class Implementation

        public static string GetLocalizationText(this string key) {
            return Localization.Translate(key);
        }
        
        public static string GetLocalizationTextRandom(this string key) {
            return Localization.TranslateRandom(key);
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

        #endregion
    }
}