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

        #region Private Fields

        private object _selectedItem;

        #endregion

        #region Unity Events

        protected override void OnEnable() {
            base.OnEnable();
            _list.ItemsUpdated += ListOnItemsUpdated;
        }

        protected override void OnDisable() {
            base.OnDisable();
            _list.ItemsUpdated -= ListOnItemsUpdated;
        }

        #endregion

        #region ValueReceiverBase

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _selectedSource;
        }

        public override void UpdateReceiver() {
            _selectedItem = _selectedSource.Get<object>();
            _list.SelectValue(_selectedItem);
        }

        private void ListOnItemsUpdated() {
            if (_selectedItem == null) {
                return;
            }
            
            _list.SelectValue(_selectedItem);
        }

        #endregion
    }
}