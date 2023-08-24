using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModulesSpecific : SceneModulesBase {

        #region Serialized Fields

        [SerializeField] private List<Module> _modules = new List<Module>();

        #endregion

        #region SceneModulesBase Implementation

        public override int modulesCount => _modules.Count;

        public override List<IModule> GetModules() {
            return _modules.OfType<IModule>().ToList();
        }

        protected override AssetRequest<Module> LoadSceneModule(int index) {
            var request = new AssetRequest<Module>();
            if (_modules.Count > index) {
                request.AssetLoaded(_modules[index]);
            }
            request.Finish();
            return request;
        }

        protected override void OnSceneModuleInitialized(Module module) { }

        #endregion
    }
}