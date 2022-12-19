using System;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace c1tr00z.AssistLib.Gameplay {
    public class TriggerEvents : MonoBehaviour {

        #region Nested Classes

        [Serializable]
        private class TriggerEvent : UnityEvent<Collider> {
        }

        #endregion
        
        #region Serialized Fields

        [SerializeField] private TriggerEvent _onTriggerEnterEvent;
        
        [SerializeField] private TriggerEvent _onTriggerExitEvent;

        #endregion

        #region Unity Events

        private void OnTriggerEnter(Collider other) {
            _onTriggerEnterEvent.SafeInvoke(other);
        }

        private void OnTriggerExit(Collider other) {
            _onTriggerExitEvent.SafeInvoke(other);
        }

        #endregion
    }
}