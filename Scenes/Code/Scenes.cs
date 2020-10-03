using System;
using UnityEngine;
using System.Collections;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine.SceneManagement;

namespace c1tr00z.AssistLib.SceneManagement {
    
    public class Scenes : Module {

        #region Events

        public static event Action<float> loadingProgressUpdated;

        public static event Action<SceneItem> sceneLoaded;

        #endregion

        #region Private Fields

        private AsyncOperation _sceneLoadOperation;

        private Action _onLoadingCallback;

        private SceneItem _currentSceneItem;

        private SceneItem _sceneToLoad;

        #endregion

        #region Accessors

        public SceneItem currentSceneItem {
            get {
                if (_currentSceneItem == null) {
                    _currentSceneItem = DB.Get<SceneItem>(SceneManager.GetActiveScene().name);
                }

                return _currentSceneItem;
            }
            set => _currentSceneItem = value;
        }

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
                _currentSceneItem = _sceneToLoad;
            }
        }

        public void LoadScene(SceneItem newScene, bool force = false) {

            if (currentSceneItem == newScene && !force) {
                return;
            }
            
            currentSceneItem = newScene;

            SceneManager.LoadScene(currentSceneItem.name, LoadSceneMode.Single);
            
            sceneLoaded?.Invoke(newScene);
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
            _sceneToLoad = newScene;
            SceneManager.LoadSceneAsync(newScene.name);
        }

        #endregion
    }
}