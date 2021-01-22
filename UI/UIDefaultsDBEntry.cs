using System;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    [Obsolete]
    public class UIDefaultsDBEntry : DBEntry {

        #region Serialized Fields

        [SerializeField]
        private UILayerDBEntry _mainLayer;

        [SerializeField]
        private UILayerDBEntry _defaultLayer;

        #endregion

        #region Accessors

        public UILayerDBEntry mainLayer => _mainLayer;

        public UILayerDBEntry defaultLayer => _defaultLayer;

        #endregion
    }
}