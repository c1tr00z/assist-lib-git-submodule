using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SystemModules : Modules {

        #region Private Fields

        private List<Module> _modules = new List<Module>();

        private List<ISystemModuleProcessor> _moduleProcessors = new();

        #endregion

        #region Modules

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        public override List<IModule> GetModules() {
            return _modules.OfType<IModule>().ToList();
        }

        public override async UniTask InitModules() {
            var dbEntries = DB.GetAll<SystemModuleDBEntry>();

            if (_moduleProcessors.Count == 0) {
                _moduleProcessors.AddRange(ReflectionUtils.GetTypesByInterface<ISystemModuleProcessor>()
                    .Select(Activator.CreateInstance).OfType<ISystemModuleProcessor>());
            }
            
            dbEntries.Sort(e => e.priority);
            
            foreach (var dbEntry in dbEntries) {
                Debug.Log($"[MODULES] Initialize {dbEntry.name}");

                var module = await dbEntry.InstantiatePrefabAsync<Module>();
                module.name = dbEntry.name;
                module.transform.Reset(transform);
                
                foreach (var p in _moduleProcessors) {
                    await p.PreInitProcess(module);
                }
                
                await module.InitializeModule();
                
                OnModuleInitialized(module);
                    
                _modules.Add(module);
                
                Debug.Log($"[MODULES] {dbEntry.name} is initialized");
            }
            
            AddMe();
        }

        #endregion
    }
}