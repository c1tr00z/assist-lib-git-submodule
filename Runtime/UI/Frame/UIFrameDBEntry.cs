using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UIFrameDBEntry : DBEntry {

        #region Serialized Fields

        [SerializeField]
        private UILayerDBEntry _layer;

        #endregion

        #region Accessors

        public UILayerDBEntry layer => _layer;

        #endregion
    }
}