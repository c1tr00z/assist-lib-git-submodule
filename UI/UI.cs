using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    /**
     * <summary>Main class for UI subsystem. Hierarchy is UI has several layers, each layer can have one or several
     * frames (depends on implementation). Uses resource management subsystem (<see cref="DB"/>, <see cref="DBEntry"/>)</summary>
     */
    public class UI : Module {

        #region Private Fields

        private Dictionary<UILayerDBEntry, UILayerBase> _layers = new Dictionary<UILayerDBEntry, UILayerBase>();

        private UIDefaultsDBEntry _uiDefaults;

        private UILayerBase _defaultLayerSrc;

        #endregion

        #region Accessors

        private UIDefaultsDBEntry uiDefaults {
            get {
                if (_uiDefaults == null) {
                    _uiDefaults = DB.Get<UIDefaultsDBEntry>();
                }
                return _uiDefaults;
            }
        }

        private UILayerBase defaultLayerSrc {
            get {
                if (_defaultLayerSrc == null) {
                    _defaultLayerSrc = DB.Get<UIDefaultsDBEntry>().defaultLayer.LoadPrefab<UILayerBase>();
                }
                return _defaultLayerSrc;
            }
        }

        #endregion

        #region Class Implementation

        public void Show(UIFrameDBEntry newFrame) {
            Show(newFrame, null);
        }

        public void Show(UIFrameDBEntry newFrame, params object[] args) {
            var requiredLayer = GetOrCreateLayer(newFrame.layer);
            requiredLayer.Show(newFrame, args);
        }

        private UILayerBase GetOrCreateLayer(UILayerDBEntry layerDBEntry) {
            if (layerDBEntry == null) {
                return GetOrCreateLayer(uiDefaults.mainLayer);
            }
            if (_layers.ContainsKey(layerDBEntry)) {
                return _layers[layerDBEntry];
            }
            var existedButNotCached = GetComponentsInChildren<UILayerBase>().First(l => l.layerDBEntry == layerDBEntry);
            if (existedButNotCached != null) {
                _layers.AddOrSet(layerDBEntry, existedButNotCached);
                return existedButNotCached;
            }
            return CreateLayer(layerDBEntry);
        }

        private UILayerBase CreateLayer(UILayerDBEntry layerDBEntry) {
            var layerPrefab = layerDBEntry.LoadPrefab<UILayerBase>();
            if (layerPrefab == null) {
                layerPrefab = defaultLayerSrc;
            }
            var layer = layerPrefab.Clone(transform);
            layer.Init(layerDBEntry);
            _layers.Add(layerDBEntry, layer);
            transform.SetChildrenSiblingIndex(c => {
                var canvas = c.GetComponent<Canvas>();
                if (canvas == null) {
                    return 1000;
                }

                return c.GetComponent<Canvas>().sortingOrder;
            });
            return layer;
        }

        public IEnumerable<UILayerBase> GetLayersOnTop(UILayerBase layer, bool include) {
            var layersList = new List<UILayerBase>();
            if (include) {
                layersList.Add(layer);
            }
            layersList.AddRange(_layers.Values.Where(l => l.canvas.sortingOrder >= layer.canvas.sortingOrder && l != layer && l.usedByHotkeys));
            return layersList;
        }

        public bool IsTopFrameInStack(UIFrame frame) {
            var layers = GetLayersOnTop(frame.layer, false);
            return layers.Where(l => l.currentFrames.Count > 0).Count() == 0;
        }
        
        public void CloseAllFrames() {
            _layers.Values.ToList().ForEach(l => l.CloseAll());
        }

        #endregion
    }
}