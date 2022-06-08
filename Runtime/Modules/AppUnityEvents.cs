using System;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.AppModules {
    public class AppUnityEvents : MonoBehaviour {

        #region Public Fields

        public UnityEvent OnInitialized;

        #endregion

        #region Unity Events

        private void OnEnable() {
            App.Initialized += AppOnInitialized;
        }

        private void OnDisable() {
            App.Initialized -= AppOnInitialized;
        }

        private void Start() {
            if (!App.isInitialized) {
                return;
            } 
            
            AppOnInitialized();
        }

        #endregion

        #region Class Implementation
        
        private void AppOnInitialized() {
            OnInitialized.SafeInvoke();
        }

        #endregion
    }
}