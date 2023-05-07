using AssistLib.Editor.DB;
using c1tr00z.AssistLib.Localization;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace AssistLib.Editor.Localization {
    public class DBEntryCheckerLanguageItem : DBEntryChecker<LanguageItem> {

        protected override void CheckEntry(LanguageItem dbEntry, AddressableAssetSettings addressableSettings, string groupName) {
            FindEntry<TextAsset>(dbEntry, "Text", addressableSettings, groupName);
        }
    }
}