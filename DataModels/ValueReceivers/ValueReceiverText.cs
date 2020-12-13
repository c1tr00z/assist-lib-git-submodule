using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    [RequireComponent(typeof(Text))]
    public class ValueReceiverText : ValueReceiverBase {

        #region Serialized Fields

        [ReferenceType(typeof(string))]
        [SerializeField]
        private PropertyReference _textSource;

        #endregion

        #region Private Fields

        private Text _text;

        #endregion

        #region Accesors

        public Text text => this.GetCachedComponent(ref _text);

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