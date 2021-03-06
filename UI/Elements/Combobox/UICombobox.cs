﻿using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.GameUI {
    public class UICombobox : MonoBehaviour {

        #region Private Fields

        private RectTransform _rectTransform;

        private Transform _cancelObjectParent;
        
        private Transform _optionsObjectParent;

        #endregion

        #region Serialized Fields

        [SerializeField] private UnityEvent _onSelected;

        [SerializeField] private RectTransform _cancelObject;

        [SerializeField] private RectTransform _optionsObject;
        
        [SerializeField] private UIList _optionsList;
        
        [SerializeField] private UIListItem _currentValue;

        #endregion

        #region Accessors

        public object selectedValue { get; private set; }
        
        public bool isShowOptions { get; private set; }
        
        public RectTransform rectTransform => this.GetCachedComponent(ref _rectTransform);

        #endregion

        #region Unity Fields
        
        private void Start() {
            UpdateControls();
            OnSelected();
        }

        #endregion

        #region Class Implementation

        public void UpdateCombobox(List<object> options, object selectedValue) {
            this.selectedValue = selectedValue;
            _optionsList.UpdateList(options, selectedValue);
            UpdateControls();
        }

        public void OnSelected() {
            Debug.LogError(selectedValue);
            selectedValue = _optionsList.selectedValue;
            _currentValue.gameObject.SetActive(true);
            _onSelected?.Invoke();
            Debug.LogError(selectedValue);
            UpdateControls();
            ResetControls();
        }

        public void ResetControls() {
            isShowOptions = false;
            _currentValue.gameObject.SetActive(!isShowOptions);
            UpdateControls();
            AttachControls();
        }

        public void ShowOptions() {
            isShowOptions = true;
            _currentValue.gameObject.SetActive(!isShowOptions);
            UpdateControls();
            DetachControls();
        }

        private void DetachControls() {
            if (_cancelObject != null) {
                _cancelObjectParent = _cancelObject.parent;
                _cancelObject.SetParent(GetComponentInParent<Canvas>().transform);
                _cancelObject.SetAsLastSibling();
            }

            if (_optionsObject != null) {
                _optionsObjectParent = _optionsObject.parent;
                _optionsObject.SetParent(GetComponentInParent<Canvas>().transform);
                _optionsObject.SetAsLastSibling();
            }
        }
        
        private void AttachControls() {
            if (_cancelObject != null && _cancelObjectParent != null) {
                _cancelObject.SetParent(_cancelObjectParent);
                _cancelObject.SetAsLastSibling();
            }

            if (_optionsObject != null && _optionsObjectParent != null) {
                _optionsObject.SetParent(_optionsObjectParent);
                _optionsObject.SetAsLastSibling();
            }
        }

        private void UpdateControls() {
            if (_cancelObject != null) {
                _cancelObject.gameObject.SetActive(isShowOptions);
            }

            if (_optionsObject != null) {
                _optionsObject.gameObject.SetActive(isShowOptions);
            }
            
            _currentValue.UpdateItem(selectedValue);
        }

        #endregion
    }
}
