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

                var prefabRequest = dbEntry.LoadPrefabAsync<Module>();

                yield return prefabRequest;

                var prefab = prefabRequest.asset;
                
                var module = prefab.Clone(transform);

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