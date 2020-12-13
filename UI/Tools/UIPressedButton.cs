using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(EventTrigger))]
    public class UIPressedButton : MonoBehaviour {

        #region Private Fields

        private bool _pressed;

        #endregion

        #region Public Fields

        public UnityEvent OnPressedEvent;

        #endregion

        #region Unity Events

        private void Update() {
            if (_pressed) {
                OnPressedEvent.Invoke();
            }
        }

        #endregion

        #region Class Implementation

        public void OnPressed() {
            _pressed = true;
        }

        public void OnReleased() {
            _pressed = false;
        }

        #endregion
    }
}