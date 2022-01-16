using System;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace c1tr00z.AssistLib.Gameplay {
    public class TriggerEvents2D : MonoBehaviour {

        #region Nested Classes

        [Serializable]
        private class TriggerEvent : UnityEvent<Collider2D> {
        }

        #endregion
        
        #region Serialized Fields

        [SerializeField] private TriggerEvent _onTriggerEnterEvent;
        
        [SerializeField] private TriggerEvent _onTriggerExitEvent;

        #endregion

        #region Unity Events

        private void OnTriggerEnter2D(Collider2D other) {
            if (_onTriggerEnterEvent.IsNull()) {
                return;
            }
            _onTriggerEnterEvent.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (_onTriggerExitEvent.IsNull()) {
                return;
            }
            _onTriggerExitEvent.Invoke(other);
        }

        #endregion
    }
}