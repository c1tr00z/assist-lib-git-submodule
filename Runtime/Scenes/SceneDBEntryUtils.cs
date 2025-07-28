using System;
using c1tr00z.AssistLib.AppModules;
using Cysharp.Threading.Tasks;

namespace c1tr00z.AssistLib.SceneManagement {
    public static class SceneDBEntryUtils {

        #region Class Implementation

        public static void Load(this SceneItem sceneDBEntry, bool force = false) {
            Modules.Get<Scenes>().LoadScene(sceneDBEntry, force);
        }
        
        public static UniTask LoadAsync(this SceneItem sceneDBEntry, bool force = false) {
            return Modules.Get<Scenes>().LoadSceneAsync(sceneDBEntry, force);
        }
        
        public static UniTask LoadAdditiveAsync(this SceneItem sceneDBEntry, bool force = false) {
            return Modules.Get<Scenes>().LoadSceneAdditiveAsync(sceneDBEntry, force);
        }

        #endregion
    }
}