using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UIDefaultsDBEntry : DBEntry {

        [SerializeField]
        private UILayerDBEntry _mainLayer;

        [SerializeField]
        private UILayerDBEntry _defaultLayer;

        public UILayerDBEntry mainLayer {
            get { return _mainLayer; }
        }

        public UILayerDBEntry defaultLayer {
            get { return _defaultLayer; }
        }
    }
}