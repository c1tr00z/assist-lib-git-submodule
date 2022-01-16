using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(Button))]
    public class UIHotkeyButton : MonoBehaviour {

        #region Public Fields

        public KeyCode key = KeyCode.Escape;

        #endregion

        #region Unity Events

        public void Update() {
            if (Input.GetKeyUp(key)) {
                CheckAndInvoke();
            }            
        }

        #endregion

        #region Class Implementation

        private void CheckAndInvoke() {
            if (GetComponentInParent<UIFrame>().isTopFrame) {
                GetComponent<Button>().onClick.Invoke();
            }
        }

        #endregion
    }
}