using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    
    public abstract class Modules : MonoBehaviour {

        #region Private Fields

        private static List<Modules> _modules = new List<Modules>();

        #endregion

        #region Unity Events

        protected virtual void Awake() {
            _modules.Add(this);
        }

        #endregion

        #region Class Implementation

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
            return _modules.SelectNotNull(m => m.GetModule<T>()).FirstOrDefault();
        }

        #endregion
    }
}