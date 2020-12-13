using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverImage : ValueReceiverBase {
        #region Serialized Fields

        [ReferenceType(typeof(Sprite))]
        [SerializeField] private PropertyReference _spriteSrc;

        [SerializeField] private Image _image;

        #endregion

        #region ValueReceiverBase Implementation

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _spriteSrc;
        }

        public override void UpdateReceiver() {
            _image.sprite = _spriteSrc.Get<Sprite>();
        }

        #endregion
    }
}