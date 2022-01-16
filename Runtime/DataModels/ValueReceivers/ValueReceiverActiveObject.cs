using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {

    public class ValueReceiverActiveObject : ValueReceiverBase {

        #region Serialized Fields

        [ReferenceTypeAttribute(typeof(bool))]
        [SerializeField] private PropertyReference _isActiveSrc;
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _inverse;

        #endregion

        #region ValueReceiverBase Implementation

        public override void UpdateReceiver() {
            _target.SetActive(!_inverse ? _isActiveSrc.Get<bool>() : !_isActiveSrc.Get<bool>());
        }

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _isActiveSrc;
        }

        #endregion
    }
}