using System;
using c1tr00z.AssistLib.AppModules;
using UnityEngine;

namespace c1tr00z.AssistLib.SceneManagement {
    public class LoadScene : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private SceneItem _sceneItem;

        [SerializeField] private bool _loadOnStart;

        #endregion

        #region Unity Events

        private void Start() {
            if (_loadOnStart) {
                Load();
            }
        }

        #endregion

        #region Class Implementation

        private void Load() {
            Modules.Get<Scenes>().LoadSceneAsync(_sceneItem);
        }

        #endregion

    }
}