using c1tr00z.AssistLib.AppModules;

namespace c1tr00z.AssistLib.GameUI {
    public static class UIFrameDBEntryUtils {

        #region Class Implementation

        public static void Show(this UIFrameDBEntry frameDBEntry, params object[] args) {
            Modules.Get<UI>().Show(frameDBEntry, args);
        }

        public static void Hide(this UIFrameDBEntry frameDBEntry) {
            Modules.Get<UI>().Hide(frameDBEntry);
        }

        #endregion
    }
}