using System.Collections.Generic;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(RectTransform))]
    public abstract class UILayerBase : MonoBehaviour {

        #region Private Fields

        private RectTransform _rectTransform;
        
        private Canvas _canvas;

        private DBEntryResource _dbEntryResource;

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
        
        public abstract List<UIFrame> currentFrames { get; }

        #endregion

        #region Class Implementation

        public void Init(UILayerDBEntry layerDBEntry) {
            name = layerDBEntry.name;
            canvas.sortingOrder = layerDBEntry.sortOrder;
        }

        protected UIFrame ShowFrame(UIFrameDBEntry frameItem, params object[] args) {
            var frame = frameItem.LoadFrame().Clone(rectTransform);
            frame.Show(args);
            frame.rectTransform.Stretch();

            return frame;
        }

        public void CloseAll() {
            currentFrames.ForEach(f => Close(f.GetComponent<DBEntryResource>().parent as UIFrameDBEntry));
        }

        #endregion

        #region Abstract Methods

        public abstract void Show(UIFrameDBEntry frame, params object[] args);

        public abstract void Close(UIFrameDBEntry frameDBEntry);

        #endregion
    }
}
