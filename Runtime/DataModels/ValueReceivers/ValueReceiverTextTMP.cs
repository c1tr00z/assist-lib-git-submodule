using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.Utils;
using TMPro;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverTextTMP : ValueReceiverBase {
        
        #region Serialized Fields

        [ReferenceType(typeof(string))]
        [SerializeField]
        private PropertyReference _textSource;

        #endregion

        #region Private Fields

        private TMP_Text _text;

        #endregion

        #region Accesors

        public TMP_Text text => this.GetCachedComponent(ref _text);

        #endregion

        #region ValueReceiverBase Implementation

        public override void UpdateReceiver() {
            text.text = _textSource.Get<string>();
        }
        
        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _textSource;
        }

        #endregion
    }
}