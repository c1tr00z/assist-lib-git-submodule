using System.Collections.Generic;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.AssistLib.PropertyReferences;

namespace c1tr00z.AssistLib.GameUI {
    public class UIItemViewArg : ValueReceiverBase {

        [ReferenceType(typeof(object))] 
        public PropertyReference argSrc;
        
        public override void UpdateReceiver() {
            GetComponents<IUIView>().ForEach(view => view.Show(argSrc.Get<object>()));
        }

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return argSrc;
        }
    }
}