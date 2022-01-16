using System;
using UnityEngine;
using System.Collections;
using c1tr00z.AssistLib.Common;

namespace c1tr00z.AssistLib.AppModules {
    public class App : BehaviourSingleton<App> {

        #region Events

        public static event Action Initialized;

        #endregion

        #region Accessors

        public bool isInitialized { get; private set; } = false;

        #endregion

        #region Unity Events

        IEnumerator Start() {

            DontDestroyOnLoad(gameObject);

            yield return StartCoroutine(C_Initialize());
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_Initialize() {
            Debug.Log("Init cachers");
            var mainCacher = gameObject.AddComponent<MainCacher>();
            yield return mainCacher.Cache();
            Debug.Log("Cachers initialized");

            Debug.Log("System modules initialization");
            var systemModules = new GameObject("SystemModules").AddComponent<SystemModules>();
            yield return systemModules.InitModules();
            Debug.Log("System modules initialized");
        }

        #endregion
    }
}