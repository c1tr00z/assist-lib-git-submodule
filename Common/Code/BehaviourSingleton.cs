using System;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    [Obsolete("Logic replaced with Modules subsystem")]
    public abstract class BehaviourSingleton<T> : MonoBehaviour where T: MonoBehaviour {

        #region Private Fields
        
        private static T _instance;

        #endregion

        #region Accessors

        public static T instance {
            get {
                TryFind();
                return _instance;
            }
        }

        #endregion

        #region Unity Events

        protected virtual void Awake() {
            if (_instance == null || _instance.Equals(default(MonoBehaviour))) {
                _instance = GetThis();
            } else if (!_instance.Equals(GetThis())) {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Class Implementation

        private static void TryFind() {
            if (_instance == null) {
                _instance = FindObjectOfType<T>();
            }
        }

        protected virtual T GetThis() {
            return this as T;
        }

        #endregion
    }
}
