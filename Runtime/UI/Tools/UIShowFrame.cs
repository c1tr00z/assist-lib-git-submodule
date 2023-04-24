using System.Collections;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UIShowFrame : MonoBehaviour {

        #region Serialized Fields

        [SerializeField]
        [DBEntryType(typeof(UIFrameDBEntry))]
        private DBEntryReference _frameDBEntryRef;

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
            if (!_frameDBEntryRef.IsValid()) {
                return;
            }

            var frameDBEntry = _frameDBEntryRef.GetDBEntry<UIFrameDBEntry>();

            var args = _argsSources == null
                ? new object[0]
                : _argsSources.SelectNotNull(s => s.Get<object>()).ToArray();
            frameDBEntry.Show(args);
        }

        #endregion
    }
}