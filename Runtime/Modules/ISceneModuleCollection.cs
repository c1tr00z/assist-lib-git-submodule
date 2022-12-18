using System.Collections.Generic;

namespace c1tr00z.AssistLib.AppModules {
    public interface ISceneModuleCollection {
        
        #region Methods

        public List<SceneModuleDBEntry> GetModuleDBEntries();

        public SceneModuleDBEntry Get(int index);

        public bool Has(int index);

        #endregion

    }
}