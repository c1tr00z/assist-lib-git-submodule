﻿using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class UIFrameReference : MonoBehaviour {

        #region Private Fields

        private UIFrame _currentFrame;

        #endregion

        #region Serialized Fields

        [ReferenceType(typeof(object))] [SerializeField]
        private PropertyReference[] _argsSrc;

        #endregion

        #region Public Fields

        public UIFrameDBEntry frameDBEntry;

        public bool stretch;

        #endregion

        #region Unity Fields

        private void Start() {
            RespawnFrame();
        }

        #endregion

        #region Class Implementation

        private void RespawnFrame() {
            transform.DestroyAllChildren();
            var prefab = frameDBEntry.LoadPrefab<UIFrame>();
            if (prefab == null)
            {
                Debug.LogError($"Prefab for UIFrameDBEntry {frameDBEntry} is null. This is {name}.", this);
                return;
            }
            _currentFrame = frameDBEntry.LoadPrefab<UIFrame>().Clone(transform);
#if UNITY_EDITOR
            if (EditorApplication.isPlaying) {
                ShowCurrent();
            }
#else
            ShowCurrent();
#endif

            if (stretch) {
                _currentFrame.rectTransform.Stretch();
            }

#if UNITY_EDITOR
            _currentFrame.gameObject.hideFlags = HideFlags.DontSave | HideFlags.DontSaveInEditor;
#endif
        }

        private void ShowCurrent() {
            var parentFrame = GetComponentInParent<UIFrame>();
            if (parentFrame == null) {
                return;
            }
            var args = _argsSrc == null ? new object[0] : _argsSrc.SelectNotNull(src => src.Get<object>()).ToArray();
            _currentFrame.Show(parentFrame.layer, args);
        }

        #endregion
    }
}