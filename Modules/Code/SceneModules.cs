using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModules : Modules {

        #region Serialized Fields

        [SerializeField] private List<Module> _modules = new List<Module>();

        #endregion
        
        #region Modules Implementation

        public override List<IModule> GetModules() {
            return _modules.Select(m => m as IModule).ToList();
        }

        #endregion
        
    }
}