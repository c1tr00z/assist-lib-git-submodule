using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModules : Modules {

        #region Serialized Fields

        [SerializeField] private List<Module> _modules = new List<Module>();

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