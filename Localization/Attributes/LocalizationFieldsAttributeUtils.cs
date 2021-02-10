using System.Linq;
using System.Reflection;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.Localization {
    public static class LocalizationFieldsAttributeUtils {

        #region Class Implementation

        public static LocalizationFieldsAttribute GetLocalizationAttribute(this FieldInfo fieldInfo) {
            return fieldInfo.GetCustomAttributes().OfType<LocalizationFieldsAttribute>().FirstOrDefault();
        }

        public static string GetLocalizationKey(this FieldInfo fieldInfo) {
            var attribute = fieldInfo.GetLocalizationAttribute();
            return attribute.localizationKey.IsNullOrEmpty() ? fieldInfo.Name : attribute.localizationKey;
        }

        #endregion
    }
}