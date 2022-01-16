using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common.Coroutines;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    
    public abstract class Modules : MonoBehaviour {

        #region Private Fields

        private static List<Modules> _modules = new List<Modules>();

        #endregion

        #region Unity Events

        protected virtual void Awake() {
            if (_modules.Contains(this)) {
                return;
            }
            _modules.Add(this);
        }

        #endregion

        #region Class Implementation

        public abstract CoroutineRequest InitModules();

        protected List<Modules> GetModulesModules() {
            return _modules.ToList();
        }

        public abstract List<IModule> GetModules();

        public List<T> GetModules<T>() where T : IModule {
            return GetModules().OfType<T>().ToList();
        }

        public T GetModule<T>() where T : IModule {
            return GetModules<T>().FirstOrDefault();
        }

        #endregion

        #region Static Methods

        public static T Get<T>() where T : IModule {
            if (_modules.Count == 0) {
                _modules.AddRange(FindObjectsOfType<Modules>());
            }
            return _modules.SelectNotNull(m => m.GetModule<T>()).FirstOrDefault();
        }

        private void OnDestroy() {
            if (_modules.Contains(this)) {
                _modules.Remove(this);
            }
        }

        #endregion
    }
}