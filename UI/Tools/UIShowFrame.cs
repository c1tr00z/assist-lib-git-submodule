using System.Collections;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UIShowFrame : MonoBehaviour {

        #region Serialized Fields

        [SerializeField]
        private UIFrameDBEntry _frameDBEntry;

        [SerializeField]
        private bool _showOnStart = false;
        
        [ReferenceType(typeof(object))]
        [SerializeField]
        private PropertyReference[] _argsSources;

        #endregion

        #region Unity Events

        private void Start() {

            if (_showOnStart) {
                Show();
            }
        }

        #endregion

        #region Class Implementation

        public void Show() {
            if (!_frameDBEntry.IsAssigned()) {
                return;
            }

            var args = _argsSources == null
                ? new object[0]
                : _argsSources.SelectNotNull(s => s.Get<object>()).ToArray();
            _frameDBEntry.Show(args);
        }

        #endregion
    }
}