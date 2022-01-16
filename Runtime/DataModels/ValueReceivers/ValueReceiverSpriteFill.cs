using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverSpriteFill : ValueReceiverBase {

        #region Serialized Fields

        [ReferenceType(typeof(float))]
        [SerializeField] private PropertyReference _fillSrc;

        [SerializeField] private Image _image;

        #endregion

        #region ValueReceiverBase Implementation

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _fillSrc;
        }

        public override void UpdateReceiver() {
            _image.fillAmount = _fillSrc.Get<float>();
        }

        #endregion
    }
}