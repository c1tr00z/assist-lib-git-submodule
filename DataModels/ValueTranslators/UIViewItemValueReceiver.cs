using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {
    public class UIViewItemValueReceiver : ValueReceiverBase {

        #region Private Fields

        private List<UIView> _views = new List<UIView>();

        #endregion

        #region Serialized Fields

        [ReferenceType(typeof(object))]
        [SerializeField] private PropertyReference _itemSrc;

        #endregion

        #region Accessors

        private List<UIView> views {
            get {
                if (_views.Count == 0) {
                    _views = GetComponents<UIView>().ToList();
                }

                return _views;
            }
        }

        #endregion

        #region ValueReceiverBase Implementation

        public override void UpdateReceiver() {
            var item = _itemSrc.Get<object>();
            views.ForEach(v =>v.Show(item));
        }

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _itemSrc;
        }

        #endregion
    }
}