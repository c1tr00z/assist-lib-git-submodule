using System;
using UnityEngine;
using System.Collections;
using c1tr00z.AssistLib.Common;
using Cysharp.Threading.Tasks;

namespace c1tr00z.AssistLib.AppModules {
    public class App : BehaviourSingleton<App> {

        #region Events

        public static event Action Initialized;

        #endregion

        #region Accessors

        public static bool isInitialized { get; private set; } = false;

        #endregion

        #region Unity Events

        void Start() {

            DontDestroyOnLoad(gameObject);
            
            Initialize().Forget();
        }

        #endregion

        #region Class Implementation

        private async UniTask Initialize() {
            Debug.Log("Init cachers");
            var mainCacher = gameObject.AddComponent<MainCacher>();
            await mainCacher.Cache();
            Debug.Log("Cachers initialized");

            new GameObject("SceneModulesHelper").AddComponent<SceneModulesHelper>();
            
            Debug.Log("System modules initialization");
            var systemModules = new GameObject("SystemModules").AddComponent<SystemModules>();
            await systemModules.InitModules();
            Debug.Log("System modules initialized");
            isInitialized = true;
            Initialized?.Invoke();
        }

        #endregion
    }
}