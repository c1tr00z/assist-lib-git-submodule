using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.AssistLib {
    public class UIFrameSceneButton : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private SceneItem _scene;
        [SerializeField] private UIFrameDBEntry _frame;

        #endregion

        #region Class Implementation

        public void Load() {
            if (_scene != null) {
                Modules.Get<Scenes>().LoadScene(_scene);
            }
            if (_frame != null) {
                Modules.Get<UI>().Show(_frame);
            }
        }

        #endregion
    }
}
