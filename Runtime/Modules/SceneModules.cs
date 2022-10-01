using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModules : Modules {

        #region Serialized Fields

        [SerializeField] private List<Module> _modules = new List<Module>();

        #endregion

        #region Unity Events

        protected override void Awake() {
            if (amIAddedAlready) {
                base.Awake();
                return;
            }
            StartCoroutine(C_InitSceneModules());
        }

        #endregion
        
        #region Modules Implementation

        public override CoroutineRequest InitModules() {
            var request = new CoroutineRequest();

            StartCoroutine(C_InitModules(request));
            
            return request;
        }

        public override List<IModule> GetModules() {
            return _modules.Select(m => m as IModule).ToList();
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_InitSceneModules() {
            base.Awake();
            var request = new CoroutineRequest();
            StartCoroutine(C_InitModules(request));
            yield return request;
        }

        private IEnumerator C_InitModules(CoroutineRequest request) {
            
            foreach (var module in _modules) {
                var moduleRequest = new CoroutineRequest();
                
                module.InitializeModule(moduleRequest);

                yield return moduleRequest;
            }
            
            request.Finish();
        }

        #endregion
        
    }
}