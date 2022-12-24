using System;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
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
        
        public override List<IModule> GetModules() {
            return _modules;
        }

        protected override AssetRequest<Module> LoadSceneModule(int index) {
            if (!modulesCollection.Has(index)) {
                var request = new AssetRequest<Module>();
                request.Finish();
                return request;
            }

            return LoadAndInstantiateModule(modulesCollection.Get(index));
        }

        private AssetRequest<Module> LoadAndInstantiateModule(SceneModuleDBEntry moduleDBEntry) {
            var request = new AssetRequest<Module>();

            StartCoroutine(C_LoadAndInstantiateModule(moduleDBEntry, request));
            
            return request;
        }

        private IEnumerator C_LoadAndInstantiateModule(SceneModuleDBEntry moduleDBEntry, AssetRequest<Module> moduleRequest) {
            var request = moduleDBEntry.LoadPrefabAsync<Module>();

            yield return request;

            var module = request.asset.Clone();
            module.name = moduleDBEntry.name;
            
            moduleRequest.AssetLoaded(module);
        }

        protected override void OnSceneModuleInitialized(Module module) {
            module.transform.Reset(transform);
            _modules.Add(module);
        }
        
        #endregion
    }
}