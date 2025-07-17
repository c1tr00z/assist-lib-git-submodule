using System;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModules : SceneModulesBase {

        #region Private Fields

        private SceneModulesCollectionMain _mainCollection;

        private List<IModule> _modules = new();

        #endregion

        #region Serialized Fields

        [SerializeField] private SceneModulesCollection _modulesCollection;

        #endregion

        #region Accessors

        private ISceneModuleCollection modulesCollection => !_modulesCollection.IsNull() ? _modulesCollection : DBEntryUtils.GetCached(ref _mainCollection);

        #endregion
        
        #region SceneModulesBase Implementation

        public override int modulesCount => modulesCollection.count;

        public override List<IModule> GetModules() {
            return _modules;
        }

        protected override UniTask<Module> LoadSceneModule(int index) {
            if (!modulesCollection.Has(index)) {
                return new UniTask<Module>();
            }

            return LoadAndInstantiateModule(modulesCollection.Get(index));
        }

        private async UniTask<Module> LoadAndInstantiateModule(SceneModuleDBEntry moduleDBEntry) {
            var module =  await moduleDBEntry.InstantiatePrefabAsync<Module>();
            module.name = moduleDBEntry.name;
            
            return module;
        }

        protected override void OnSceneModuleInitialized(Module module) {
            module.transform.Reset(transform);
            _modules.Add(module);
        }
        
        #endregion
    }
}