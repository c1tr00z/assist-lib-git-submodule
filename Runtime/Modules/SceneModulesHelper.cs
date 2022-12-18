using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModulesHelper : MonoBehaviour {

        #region Unity Events

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start() {
            while (!App.isInitialized) {
                yield return null;
            }
            AttemptToInitializeModules();
        }

        private void OnEnable() {
            Scenes.sceneLoaded += ScenesOnSceneLoaded;
        }

        private void OnDisable() {
            Scenes.sceneLoaded -= ScenesOnSceneLoaded;
        }

        #endregion

        #region Class Implementation

        private void ScenesOnSceneLoaded(SceneItem scene) {
            AttemptToInitializeModules();
        }

        private void AttemptToInitializeModules() {
            var sceneModules = FindObjectsOfType<SceneModulesBase>();
            var notInitialized = sceneModules.Where(ms => !ms.isInitialized).ToList();

            if (notInitialized.Count == 0) {
                return;
            }

            StartCoroutine(C_Initialize(notInitialized));
        }

        private IEnumerator C_Initialize(List<SceneModulesBase> sceneModules) {
            var common = sceneModules.OfType<SceneModules>().ToList();
            var specific = sceneModules.OfType<SceneModulesSpecific>().ToList();

            foreach (var modulesContainer in common) {
                yield return modulesContainer.InitModules();
            }
            
            foreach (var modulesContainer in specific) {
                yield return modulesContainer.InitModules();
            }
        }

        #endregion
    }
}