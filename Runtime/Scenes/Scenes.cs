using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace c1tr00z.AssistLib.SceneManagement {
    
    public class Scenes : Module {

        #region Events

        public static event Action<float> loadingProgressUpdated;

        public static event Action<SceneItem> sceneStartedToLoad;
        public static event Action<SceneItem> sceneLoaded;

        #endregion

        #region Private Fields

        private AsyncOperation _sceneLoadOperation;

        // private Action _onLoadingCallback;
        private bool _loadingInProgress = false;

        private SceneItem _currentSceneItem;

        private List<SceneItem> _additiveScenes = new();

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
            }
        }

        public void LoadScene(SceneItem newScene, bool force = false) {

            if (currentSceneItem == newScene && !force) {
                return;
            }
            
            currentSceneItem = newScene;

            sceneStartedToLoad?.Invoke(newScene);
            
            SceneManager.LoadScene(currentSceneItem.name, LoadSceneMode.Single);
            
            sceneLoaded?.Invoke(currentSceneItem);
        }

        public async UniTask LoadSceneAsync(SceneItem newScene, bool force = false) {

            while (_loadingInProgress) {
                await UniTask.DelayFrame(1);
            }
            
            if (currentSceneItem == newScene && !force) {
                return;
            }

            sceneStartedToLoad?.Invoke(newScene);
            _sceneLoadOperation = SceneManager.LoadSceneAsync(newScene.name);
            await _sceneLoadOperation;
            
            currentSceneItem = newScene;
            sceneLoaded?.Invoke(currentSceneItem);
            _loadingInProgress = false;
        }

        public async UniTask LoadSceneAdditiveAsync(SceneItem newScene, bool force = false) {
            
            while (_loadingInProgress) {
                await UniTask.DelayFrame(1);
            }

            if (_additiveScenes.Contains(newScene) && !force) {
                return;
            }
            
            sceneStartedToLoad?.Invoke(newScene);
            _sceneLoadOperation = SceneManager.LoadSceneAsync(newScene.name, LoadSceneMode.Additive);
            await _sceneLoadOperation;

            if (!_additiveScenes.Contains(newScene)) {
                _additiveScenes.Add(newScene);
            }
            
            sceneLoaded?.Invoke(currentSceneItem);
            _loadingInProgress = false;
        }

        #endregion
    }
}
