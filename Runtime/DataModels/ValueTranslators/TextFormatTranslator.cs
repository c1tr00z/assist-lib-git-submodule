using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {

    public class TextFormatTranslator : DataTranslator {

        #region Serialized Fields

        [ReferenceType(typeof(object))] [SerializeField]
        private PropertyReference[] _textSources;

        [SerializeField] private string _format;

        #endregion

        #region Accessors

        public string text { get; private set; }

        #endregion

        #region DataTranslator Implementation

        public override void UpdateReceiver() {
            var formatParams = _textSources.Select(s => s.Get<object>()).ToArray();
            text = string.Format(_format, formatParams);
            OnDataChanged();
        }

        public override IEnumerator<PropertyReference> GetReferences() {
            foreach (var source in _textSources) {
                yield return source;
            }
        }

        #endregion
    }
}
