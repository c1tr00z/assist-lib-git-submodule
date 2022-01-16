using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common.Coroutines;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SystemModules : Modules {

        #region Private Fields

        private List<Module> _modules = new List<Module>();

        #endregion

        #region Modules

        protected override void Awake() {
            if (GetModulesModules().Any(mm => mm.GetType() == GetType() && mm != this)) {    
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(this);
            base.Awake();
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
            var dbEntries = DB.GetAll<ModuleDBEntry>();
            
            foreach (var dbEntry in dbEntries) {
                Debug.Log($"[MODULES] Initialize {dbEntry.name}");
                
                var prefab = dbEntry.LoadPrefab<Module>();
                var module = prefab.Clone(transform);
                
                yield return module.InitializeModule();
                    
                _modules.Add(module);
                
                Debug.Log($"[MODULES] {dbEntry.name} is initialized");
            }
            
            request.Finish();
        }

        #endregion
    }
}