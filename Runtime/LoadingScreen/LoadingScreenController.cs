using System;
using System.Collections;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.LoadingScreen {
    public class LoadingScreenController : Module {

        #region Events

        public static event Action<float> ProgressUpdated;

        #endregion
        
        #region Private Fields

        private float _startLoadingTime;

        private UIFrameDBEntry _loadingScreenFrame;

        #endregion

        #region Serialized Fields

        [DBEntryType(typeof(UIFrameDBEntry))]
        [SerializeField] private DBEntryReference _loadingScreenFrameRef;

        [SerializeField] private float _minTime;

        #endregion

        #region Accessors

        private UIFrameDBEntry loadingScreenFrame {
            get {
                if (_loadingScreenFrame == null) {
                    _loadingScreenFrame = _loadingScreenFrameRef.GetDBEntry<UIFrameDBEntry>();
                }

                return _loadingScreenFrame;
            }
        }
        
        public float loadingScreenProgress { get; private set; }

        #endregion

        #region Class Implementation

        public void ShowLoadingScreen() {
            _startLoadingTime = Time.time;
            loadingScreenFrame.Show();
        }
        
        public void HideLoadingScreen() {
            StopCoroutine(nameof(C_HideLoadingScreen));
            StartCoroutine(nameof(C_HideLoadingScreen));
        }

        private IEnumerator C_HideLoadingScreen() {
            while (Time.time - _startLoadingTime < _minTime) {
                yield return null;
            }
            loadingScreenFrame.Hide();
        }

        public void UpdateProgress(float newProgress) {
            loadingScreenProgress = newProgress;
            ProgressUpdated?.Invoke(loadingScreenProgress);
        }

        #endregion
    }
}