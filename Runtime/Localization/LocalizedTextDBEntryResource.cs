using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.Localization {
    public class LocalizedTextDBEntryResource : LocalizedText {

        #region LocalizedText Implementation

        protected override string GetLocalizedText() {
            return GetComponentInParent<DBEntryResource>().parent.GetLocalizationText(key);
        }

        #endregion
    }
}