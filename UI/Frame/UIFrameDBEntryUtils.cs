using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.GameUI {
    public static class UIFrameDBEntryUtils {

        #region Class Implementation

        public static void Show(this UIFrameDBEntry frameDBEntry, params object[] args) {
            Modules.Get<UI>().Show(frameDBEntry, args);
        }
        
        public static UIFrame LoadFrame(this UIFrameDBEntry frameDBEntry) {
            return frameDBEntry.LoadPrefab<UIFrame>();
        }

        #endregion
    }
}