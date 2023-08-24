using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.Common;
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

        private Dictionary<UILayerDBEntry, UILayer> _layers = new Dictionary<UILayerDBEntry, UILayer>();

        private UIDefaultsDBEntry _uiDefaults;

        private UILayer _defaultLayerSrc;

        #endregion

        #region Serialized Fields

        [SerializeField] private UILayer _defaultLayer;

        #endregion

        #region Module Implementation

        public override void InitializeModule(CoroutineRequest request) {
            StartCoroutine(C_InitializeModule(request));
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_InitializeModule(CoroutineRequest coroutineRequest) {

            if (_defaultLayer == null) {
                var defaultLayerSrcRequest = DB.Get<UIDefaultsDBEntry>().defaultLayer.LoadPrefabAsync<UILayer>();
            
                yield return defaultLayerSrcRequest;

                _defaultLayerSrc = defaultLayerSrcRequest.asset;
            }

            
            base.InitializeModule(coroutineRequest);
        }

        public void Show(UIFrameDBEntry newFrame) {
            Show(newFrame, null);
        }

        public void Show(UIFrameDBEntry newFrame, params object[] args) {
            var requiredLayer = GetOrCreateLayer(newFrame.layer);
            requiredLayer.Show(newFrame, args);
        }

        public void Hide(UIFrameDBEntry frame) {
            var requiredLayer = GetOrCreateLayer(frame.layer);
            requiredLayer.Hide(frame);
        }

        private UILayer GetOrCreateLayer(UILayerDBEntry layerDBEntry) {
            if (layerDBEntry.IsNull()) {
                return _defaultLayer;
            }
            if (_layers.ContainsKey(layerDBEntry)) {
                return _layers[layerDBEntry];
            }
            var existedButNotCached = GetComponentsInChildren<UILayer>().First(l => l.layerDBEntry == layerDBEntry);
            if (existedButNotCached != null) {
                _layers.AddOrSet(layerDBEntry, existedButNotCached);
                return existedButNotCached;
            }

            return _defaultLayer;
        }

        public IEnumerable<UILayer> GetLayersOnTop(UILayer layer, bool include) {
            var layersList = new List<UILayer>();
            if (include) {
                layersList.Add(layer);
            }
            layersList.AddRange(_layers.Values.Where(l => l.canvas.sortingOrder >= layer.canvas.sortingOrder && l != layer && l.usedByHotkeys));
            return layersList;
        }

        // public bool IsTopFrameInStack(UIFrame frame) {
        //     var layers = GetLayersOnTop(frame.layer, false);
        //     return layers.Where(l => l.currentFrames.Count > 0).Count() == 0;
        // }
        
        public void HideAllFrames() {
            _layers.Values.ToList().ForEach(l => l.HideCurrent());
        }

        #endregion
    }
}