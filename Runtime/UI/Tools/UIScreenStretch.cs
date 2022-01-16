using System;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class UIScreenStretch : MonoBehaviour {

        #region Private Fields

        private RectTransform _rectTransform;
        private CanvasScaler _canvasScaler;

        #endregion

        #region Serialized Fields

        [SerializeField] private bool _stretchHorizontal;
        [SerializeField] private bool _stretchVertical;

        #endregion

        #region Accessors

        public RectTransform rectTransform => this.GetCachedComponent(ref _rectTransform);

        public CanvasScaler canvasScaler => this.GetCachedComponentInParent(ref _canvasScaler);

        #endregion

        #region Unity Events

        private void Update() {

            if (!_stretchHorizontal && !_stretchVertical) {
                return;
            }
            
            if (_canvasScaler == null) {
                _canvasScaler = GetComponentInParent<CanvasScaler>();
            }

            if (canvasScaler == null) {
                return;
            }
            
            if (_rectTransform == null) {
                _rectTransform = GetComponentInParent<RectTransform>();
            }

            var scale = canvasScaler.transform.localScale;
            
            
            rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2, transform.position.z);

            if (_stretchHorizontal) {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
                    Screen.width  / canvasScaler.transform.localScale.x);
            }

            if (_stretchVertical) {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                    Screen.height / canvasScaler.transform.localScale.y);
            }
        }

        #endregion
    }
}