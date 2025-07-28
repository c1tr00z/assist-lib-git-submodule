using System;
using System.Collections;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.AppModules {
    public abstract class SceneModulesBase : Modules {

        #region Events

        public static event Action SceneModulesInitialized;

        #endregion

        #region Private Fields

        private int _loadModuleIndex = 0;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private UnityEvent onInitialized;

        #endregion

        #region Accessors
        
        public abstract int modulesCount { get; }

        #endregion

        #region Modules Implementation

        protected override void OnInitialized() {
            base.OnInitialized();
            onInitialized.SafeInvoke();
        }

        #endregion

        #region Class Implementation

        public override async UniTask InitModules() {
            if (amIAddedAlready) {
                return;
            }
            
            AddMe();
            
            _loadModuleIndex = 0;

            await InitializeModules();
        }

        private async UniTask InitializeModules() {
            
            Debug.Log("[SCENE MODULES] Initialize scene modules");

            var inProgress = true;

            while (inProgress) {
                var module = await LoadSceneModule(_loadModuleIndex);
                _loadModuleIndex++;

                if (module is null) {
                    inProgress = false;
                    continue;
                }
                
                Debug.Log($"[SCENE MODULES] Initialize: {module.name}...");
            
                await module.InitializeModule();
                
                OnModuleInitialized(module);
                
                OnSceneModuleInitialized(module);
                
                Debug.Log($"[SCENE MODULES] {module.name} initialized");
            }
            
            OnInitialized();
            
            Debug.Log("[SCENE MODULES] Scene modules initialized");
        }

        protected abstract UniTask<Module> LoadSceneModule(int index);

        protected abstract void OnSceneModuleInitialized(Module module);

        public static void OnSceneModulesInitialized() {
            SceneModulesInitialized?.Invoke();
        }

        #endregion

    }
}