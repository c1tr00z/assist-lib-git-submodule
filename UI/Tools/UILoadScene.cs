using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UILoadScene : MonoBehaviour {

        #region Public Fields

        public SceneItem sceneDBEntry;

        public bool force = false;

        #endregion

        #region Class Implementation

        public void Load() {
            sceneDBEntry.Load(force);
        }

        #endregion
    }
}