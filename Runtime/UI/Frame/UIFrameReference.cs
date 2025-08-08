using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssistLib.Runtime.UI.Tools;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class UIFrameReference : MonoBehaviour {

        #region Private Fields

        private UIFrame _currentFrame;

        private List<UIFrame> _pooledFrames = new List<UIFrame>();

        #endregion

        #region Serialized Fields

        [ReferenceType(typeof(object))] [SerializeField]
        private PropertyReference[] _argsSrc;

        #endregion

        #region Public Fields

        public UIFrameDBEntry frameDBEntry;

        public bool stretch;

        #endregion

        #region Unity Events

        private void Start() {
            RespawnFrame();
        }

        #endregion

        #region Class Implementation

        private void RespawnFrame() {
#if UNITY_EDITOR
            //TODO: make it work in editor
            return;
#endif
            DoRespawnFrame();
        }

        private async UniTask DoRespawnFrame() {
            transform.GetChildren().Where(c => !_currentFrame.IsNull() && c != _currentFrame.transform).ToList()
                .ForEach(c => Destroy(c.gameObject));

            if (_currentFrame != null) {
                ShowCurrent();
                return;
            }

            var newFrame = await frameDBEntry.InstantiatePrefabAsync<UIFrame>(); ;

            _currentFrame = newFrame;
            _currentFrame.Reset(transform);
            _currentFrame.rectTransform.Stretch();
        }

        private void ShowCurrent() {
            var parentFrame = GetComponentInParent<UIFrame>();
            if (parentFrame == null) {
                return;
            }
            var args = _argsSrc == null ? new object[0] : _argsSrc.SelectNotNull(src => src.Get<object>()).ToArray();
            UIUtils.PreShow(_currentFrame);
            _currentFrame.Show(parentFrame.layer, args);
        }

        #endregion
    }
}