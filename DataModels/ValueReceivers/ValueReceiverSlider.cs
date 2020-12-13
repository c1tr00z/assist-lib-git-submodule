using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverSlider : ValueReceiverBase {

        #region Public Fields

        [ReferenceType(typeof(float))]
        public PropertyReference valueSrc;

        public Slider slider;

        #endregion
        
        #region Accessors

        public override bool isRecieverEnabled => base.isRecieverEnabled && slider != null;

        #endregion

        #region ValueReceiverBase Implementation

        public override void UpdateReceiver() {
            slider.value = valueSrc.Get<float>();
        }
        
        public override IEnumerator<PropertyReference> GetReferences() {
            yield return valueSrc;
        }

        #endregion
    }
}