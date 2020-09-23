using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.AppModules {
    public class DBEntryModules : Modules {

        #region Private Fields

        private List<Module> _modules = new List<Module>();

        #endregion

        #region Modules

        protected override void Awake() {
            if (GetModulesModules().Any(mm => mm.GetType() == GetType())) {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(this);
            CheckModules();
            base.Awake();
        }

        public override List<IModule> GetModules() {
            CheckModules();
            return _modules.OfType<IModule>().ToList();
        }

        #endregion

        #region Class Implementation

        private void CheckModules() {
            if (_modules.Count > 0) {
                return;
            }
            InitModules();
        }

        private void InitModules() {
            var prefabs = DB.GetAll<ModuleDBEntry>().SelectNotNull(dbEntry => dbEntry.LoadPrefab<Module>());

            if (prefabs.Count == 0) {
                return;
            }
            
            prefabs.ForEach(p => {
                var module = p.Clone(transform);
                _modules.Add(module);
            });
        }

        #endregion
    }
}