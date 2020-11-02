using System;
using c1tr00z.AssistLib.AppModules;

namespace c1tr00z.AssistLib.SceneManagement {
    public static class SceneDBEntryUtils {
        public static void Load(this SceneItem sceneDBEntry, bool force = false) {
            Modules.Get<Scenes>().LoadScene(sceneDBEntry, force);
        }
        
        public static void LoadAsync(this SceneItem sceneDBEntry, Action callback = null, bool force = false) {
            Modules.Get<Scenes>().LoadSceneAsync(sceneDBEntry, callback, force);
        }
    }
}