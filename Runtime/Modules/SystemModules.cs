using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SystemModules : Modules {

        #region Private Fields

        private List<Module> _modules = new List<Module>();

        #endregion

        #region Modules

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        public override List<IModule> GetModules() {
            return _modules.OfType<IModule>().ToList();
        }

        public override CoroutineRequest InitModules() {
            var request = new CoroutineRequest();

            StartCoroutine(C_InitModules(request));
            
            return request;
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_InitModules(CoroutineRequest request) {
            var dbEntries = DB.GetAll<SystemModuleDBEntry>();
            
            foreach (var dbEntry in dbEntries) {
                Debug.Log($"[MODULES] Initialize {dbEntry.name}");

                Module module = null;
                var wait = true;
                
                dbEntry.InstantiatePrefabAsync<Module>(instantiatedPrefab => {
                    module = instantiatedPrefab;
                    module.transform.Reset(transform);
                    wait = false;
                });

                while (wait) {
                    yield return null;
                }

                var moduleRequest = new CoroutineRequest();
                
                module.InitializeModule(moduleRequest);

                yield return moduleRequest;
                
                OnModuleInitialized(module);
                    
                _modules.Add(module);
                
                Debug.Log($"[MODULES] {dbEntry.name} is initialized");
            }
            
            AddMe();
            request.Finish();
        }

        #endregion
    }
}