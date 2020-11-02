using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UILoadScene : MonoBehaviour {

        public SceneItem sceneDBEntry;

        public bool force = false;

        public void Load() {
            sceneDBEntry.Load(force);
        }
    }
}