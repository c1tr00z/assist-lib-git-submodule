using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UIFrameDBEntry : DBEntry {

        [SerializeField]
        private UILayerDBEntry _layer;

        public UILayerDBEntry layer {
            get { return _layer; }
        }
    }
}