using System.Collections.Generic;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.AssistLib.PropertyReferences;

namespace c1tr00z.AssistLib.Localization {
    public class LocalizedTextTranslator : DataTranslator {

        #region Public Fields

        [ReferenceType(typeof(string))]
        public PropertyReference localizationKeySrc;

        #endregion

        #region Accessors

        public string localizedText { get; private set; }

        #endregion

        #region DataTranslator Implementation

        public override void UpdateReceiver() {
            localizedText = localizationKeySrc.Get<string>().GetLocalizationText();
        }

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return localizationKeySrc;
        }

        #endregion
    }
}