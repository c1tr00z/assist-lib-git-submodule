using System;
using UnityEngine;
using System.Collections;
using c1tr00z.AssistLib.AppModules;
using UnityEngine.SceneManagement;

namespace c1tr00z.AssistLib.SceneManagement {
    
    public class Scenes : Module {

        #region Events

        public static event Action<float> loadingProgressUpdated;

        #endregion

        #region Private Fields

        private AsyncOperation _sceneLoadOperation;

        private Action _onLoadingCallback;

        #endregion

        #region Accessors
        
        public SceneItem currentSceneItem { get; private set; }

        #endregion


        #region Unity Events

        private void Update() {
            UpdateLoadingProgress();
        }

        #endregion

        #region Class Implementation

        private void UpdateLoadingProgress() {
            if (_sceneLoadOperation == null) {
                return;
            }
            
            loadingProgressUpdated?.Invoke(_sceneLoadOperation.progress);

            if (_sceneLoadOperation.progress >= 1) {
                _sceneLoadOperation = null;
                _onLoadingCallback?.Invoke();
                _onLoadingCallback = null;
            }
        }

        public void LoadScene(SceneItem newScene) {

            currentSceneItem = newScene;

            SceneManager.LoadScene(currentSceneItem.name, LoadSceneMode.Single);
        }

        public void LoadSceneAsync(SceneItem newScene, Action callback = null, bool force = false) {
            if (currentSceneItem == newScene && !force) {
                callback?.Invoke();
                return;
            }
            _onLoadingCallback = () => {
                currentSceneItem = newScene;
                callback?.Invoke();
            };
            SceneManager.LoadSceneAsync(newScene.name);
        }

        #endregion
    }
}