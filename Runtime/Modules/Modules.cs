using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    
    public abstract class Modules : MonoBehaviour {

        #region Private Fields

        private static List<Modules> _modulesContainers = new List<Modules>();

        #endregion

        #region Accessors
        
        protected bool amIAddedAlready => _modulesContainers.Contains(this);

        #endregion

        #region Unity Events

        protected virtual void Awake() {
            if (amIAddedAlready) {
                return;
            }
            _modulesContainers.Add(this);
        }

        #endregion

        #region Class Implementation

        public abstract CoroutineRequest InitModules();

        protected List<Modules> GetModulesModules() {
            return _modulesContainers.ToList();
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
            if (_modulesContainers.Count == 0) {
                _modulesContainers.AddRange(FindObjectsOfType<Modules>());
            }
            return _modulesContainers.SelectNotNull(m => m.GetModule<T>()).FirstOrDefault();
        }

        private void OnDestroy() {
            if (_modulesContainers.Contains(this)) {
                _modulesContainers.Remove(this);
            }
        }

        #endregion
    }
}