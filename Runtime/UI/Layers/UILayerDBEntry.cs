using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UILayerDBEntry : DBEntry {

        #region Serialized Fields

        [SerializeField] private int _sortOrder;

        [SerializeField] private bool _usedByHotkeys = true;

        #endregion

        #region Accessors

        public int sortOrder => _sortOrder;

        public bool usedByHotkeys => _usedByHotkeys;

        #endregion
    }
}
