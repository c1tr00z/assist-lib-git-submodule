using System.Collections.Generic;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.AppModules {
    public class SceneModulesCollectionMain : DBEntry, ISceneModuleCollection {

        #region Public Fields

        public List<SceneModuleDBEntry> modules = new List<SceneModuleDBEntry>();

        #endregion

        #region ISceneModuleCollection Implementation

        public List<SceneModuleDBEntry> GetModuleDBEntries() {
            return modules;
        }

        public SceneModuleDBEntry Get(int index) {
            return modules.Count > index ? modules[index] : null;
        }

        public bool Has(int index) {
            return modules.Count > index;
        }

        #endregion
    }
}