using System;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.Localization {
    public class LocalizationFieldsAttribute : Attribute {

        #region Accessors

        public string localizationKey { get; }

        #endregion

        #region Constructors

        public LocalizationFieldsAttribute(string localizationKey) {
            this.localizationKey = localizationKey;
        }
        
        public LocalizationFieldsAttribute() : this(null) {
        }

        #endregion
    }
}