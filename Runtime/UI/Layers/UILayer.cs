using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(RectTransform))]
    public class UILayer : MonoBehaviour {

        #region Nested Classes

        protected class FrameRequest {
            public UIFrameDBEntry frameDBEntry;
            public object[] args = Array.Empty<object>();
        }

        #endregion

        #region Private Fields

        private RectTransform _rectTransform;
        
        private Canvas _canvas;

        private DBEntryResource _dbEntryResource;

        private Transform _pool;

        private List<UIFrame> _pooledFrames = new List<UIFrame>();

        protected Queue<FrameRequest> _requestsQueue = new Queue<FrameRequest>();

        private bool _isShowingCoroutineOn = false;

        #endregion

        #region Accessors

        public RectTransform rectTransform {
            get {
                if (_rectTransform == null) {
                    _rectTransform = GetComponent<RectTransform>();
                }
                return _rectTransform;
            }
        }

        public Canvas canvas {
            get {
                if (_canvas == null) {
                    _canvas = GetComponent<Canvas>();
                }
                return _canvas;
            }
        }

        public UILayerDBEntry layerDBEntry => this.GetCachedComponent(ref _dbEntryResource).parent as UILayerDBEntry;

        public bool usedByHotkeys => layerDBEntry.usedByHotkeys;
        
        public UIFrame currentFrame { get; private set; }
        
        public UIFrameDBEntry currentFrameDBEntry { get; private set; }

        private Transform pool {
            get {
                if (_pool == null) {
                    _pool = new GameObject("Pool").transform;
                    _pool.Reset(transform);
                    _pool.gameObject.SetActive(false);
                }

                return _pool;
            }
        }

        #endregion

        #region Class Implementation

        public void Init(UILayerDBEntry layerDBEntry) {
            name = layerDBEntry.name;
            canvas.sortingOrder = layerDBEntry.sortOrder;
        }

        public void Show(UIFrameDBEntry frame, params object[] args) {
            if (_isShowingCoroutineOn) {
                return;
            }

            _requestsQueue.Enqueue(new FrameRequest {
                frameDBEntry = frame,
                args = args,
            });
            StartCoroutine(nameof(C_Show));
        }

        public void Close(UIFrameDBEntry frameDBEntry) {
            if (currentFrameDBEntry != frameDBEntry) {
                return;
            }

            ReturnToPool(currentFrame);
            
            currentFrameDBEntry = null;

        }

        private void ShowFrame(UIFrame frame, params object[] args) {
            if (frame.transform.parent != rectTransform) {
                frame.transform.Reset(rectTransform);
                frame.rectTransform.Stretch();
            }
            frame.Show(args);
        }

        private IEnumerator C_Show() {
            _isShowingCoroutineOn = true;
            while (_requestsQueue.Count > 0) {
                var request = _requestsQueue.Dequeue();

                if (request.frameDBEntry == currentFrameDBEntry) {
                    ShowFrame(currentFrame, request.args);
                    continue;
                }

                var frame = GetFrameFromPool(request.frameDBEntry);

                if (frame == null) {

                    var instantRequest = request.frameDBEntry.InstantiatePrefabAsync<UIFrame>();

                    yield return instantRequest;

                    frame = instantRequest.asset;
                }

                if (frame == null) {
                    continue;
                }
                
                ReturnToPool(currentFrame);
                currentFrame = frame;
                currentFrameDBEntry = currentFrame.dbEntry;
                ShowFrame(frame, request.args);
            }
            
            _isShowingCoroutineOn = false;
        }

        public void CloseCurrent() {
            Close(currentFrameDBEntry);
        }

        protected UIFrame GetFrameFromPool(UIFrameDBEntry frameDBEntry) {
            var frame = _pooledFrames.FirstOrDefault(f => f.dbEntry == frameDBEntry);

            if (frame != null) {
                _pooledFrames.Remove(frame);
            }

            return frame;
        }

        protected virtual void ReturnToPool(UIFrame frame) {
            if (frame == null) {
                return;
            }
            frame.transform.Reset(pool);
            _pooledFrames.Add(frame);
            currentFrame = null;
        }

        #endregion
    }
}
