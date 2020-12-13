using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverGraphicColor : ValueReceiverBase {
        #region Serialized Fields

        [ReferenceType(typeof(Color))]
        [SerializeField] private PropertyReference _colorSrc;

        [SerializeField] private Graphic _graphic;

        #endregion

        #region ValueReceiverBase Implementation

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _colorSrc;
        }

        public override void UpdateReceiver() {
            _graphic.color = _colorSrc.Get<Color>();
        }

        #endregion
    }
}