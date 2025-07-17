using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

        protected override UniTask<Module> LoadSceneModule(int index) {
            if (_modules.Count > index) {
                return new UniTask<Module>(_modules[index]);
            }

            return new UniTask<Module>(null);
        }

        protected override void OnSceneModuleInitialized(Module module) { }

        #endregion
    }
}