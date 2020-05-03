using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverSlider : ValueReceiverBase {
        public override bool isRecieverEnabled => base.isRecieverEnabled && slider != null;

        [ReferenceType(typeof(float))]
        public PropertyReference valueSrc;

        public Slider slider;

        public override void UpdateReceiver() {
            slider.value = valueSrc.Get<float>();
        }
        
        public override IEnumerator<PropertyReference> GetReferences() {
            yield return valueSrc;
        }
    }
}