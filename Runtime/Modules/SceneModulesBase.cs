using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.AppModules {
    public abstract class SceneModulesBase : Modules {

        #region Private Fields

        private int _loadModuleIndex = 0;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private UnityEvent onInitialized;

        #endregion

        #region Modules Implementation

        protected override void OnInitialized() {
            base.OnInitialized();
            AddMe();
            onInitialized.SafeInvoke();
        }

        #endregion

        #region Class Implementation

        public override CoroutineRequest InitModules() {
            if (amIAddedAlready) {
                return CoroutineRequest.MakeFinishedRequest();
            }
            _loadModuleIndex = 0;
            
            var request = new CoroutineRequest();

            StartCoroutine(C_InitializeModules(request));
            
            return request;
        }

        private IEnumerator C_InitializeModules(CoroutineRequest request) {

            var inProgress = true;

            while (inProgress) {
                var moduleLoadRequest = LoadSceneModule(_loadModuleIndex);
                _loadModuleIndex++;

                yield return moduleLoadRequest;

                var module = moduleLoadRequest.asset;

                if (module == null) {
                    inProgress = false;
                    continue;
                }

                var moduleRequest = new CoroutineRequest();
            
                module.InitializeModule(moduleRequest);

                yield return moduleRequest;
                
                OnModuleInitialized(module);
            }
            
            OnInitialized();
            request.Finish();
        }

        protected abstract AssetRequest<Module> LoadSceneModule(int index);

        protected abstract void OnModuleInitialized(Module module);

        #endregion

    }
}