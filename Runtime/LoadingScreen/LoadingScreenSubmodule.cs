using System.Collections;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.SceneManagement;

namespace c1tr00z.AssistLib.LoadingScreen {
    public class LoadingScreenSubmodule : Submodule<LoadingScreenController> {

        #region Unity Events

        private void OnEnable() {
            Scenes.sceneStartedToLoad += OnSceneStartedToLoad;
            Scenes.loadingProgressUpdated += OnSceneLoadingProgressUpdated;
            Scenes.sceneLoaded += OnSceneLoaded;
            SceneModulesBase.SceneModulesInitialized += OnAllSceneModulesInitialized;
        }

        private void OnDisable() {
            Scenes.sceneStartedToLoad -= OnSceneStartedToLoad;
            Scenes.loadingProgressUpdated -= OnSceneLoadingProgressUpdated;
            Scenes.sceneLoaded -= OnSceneLoaded;
            SceneModulesBase.SceneModulesInitialized -= OnAllSceneModulesInitialized;
        }

        #endregion

        #region Class Implementation

        private void OnSceneStartedToLoad(SceneItem scene) {
            module.ShowLoadingScreen();
        }

        private void OnSceneLoadingProgressUpdated(float progress) {
            module.UpdateProgress(progress / 2);
        }

        private void OnSceneLoaded(SceneItem scene) {
            StopCoroutine(nameof(C_ModulesCheck));
            StartCoroutine(nameof(C_ModulesCheck));
        }

        private IEnumerator C_ModulesCheck() {
            var sceneModules = FindObjectsOfType<SceneModulesBase>();

            if (sceneModules.Length > 0) {
                var divider = 2 * sceneModules.Length;
                var smInitedCount = 0;
                foreach (var sm in sceneModules) {
                    var modulesCount = sm.modulesCount;
                    while (!sm.isInitialized) {
                        var modulesProgress = (sm.GetModules().Count * 1f / modulesCount + smInitedCount) / divider;
                        module.UpdateProgress(0.5f + modulesProgress);
                        yield return null;
                    }

                    smInitedCount++;
                }
            }
        }

        private void OnAllSceneModulesInitialized() {
            module.UpdateProgress(1);
            module.HideLoadingScreen();
        }

        #endregion
    }
}