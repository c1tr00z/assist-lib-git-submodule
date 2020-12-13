using System.Collections.Generic;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverListSelected : ValueReceiverBase {
        
        #region Serialized Fields

        [ReferenceType(typeof(object))]
        [SerializeField]
        private PropertyReference _selectedSource;

        [SerializeField] private UIList _list;

        #endregion

        #region ValueReceiverBase

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _selectedSource;
        }

        public override void UpdateReceiver() {
            _list.SelectValue(_selectedSource.Get<object>());
        }

        #endregion
    }
}