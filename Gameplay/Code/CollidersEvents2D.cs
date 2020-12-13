using System;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.Gameplay {
    /**
     * Gameplay class for translate OnCollider events to other gameObject
     */
    public class CollidersEvents2D : MonoBehaviour {
        #region Nested Classes

        [Serializable]
        private class ColliderEvent : UnityEvent<Collider2D> {
        }

        #endregion
        
        #region Serialized Fields

        [SerializeField] private ColliderEvent _onCollisionEnterEvent;
        
        [SerializeField] private ColliderEvent _onCollisionExitEvent;

        #endregion

        #region Unity Events

        private void OnCollisionEnter2D(Collision2D other) {
            if (_onCollisionEnterEvent.IsAssigned()) {
                _onCollisionEnterEvent?.Invoke(other.collider);
            }
        }

        private void OnCollisionExit2D(Collision2D other) {
            if (_onCollisionExitEvent.IsAssigned()) {
                _onCollisionExitEvent?.Invoke(other.collider);
            }
        }

        #endregion
    }
}