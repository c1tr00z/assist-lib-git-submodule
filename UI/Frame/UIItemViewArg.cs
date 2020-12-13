using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.AssistLib.PropertyReferences;

namespace c1tr00z.AssistLib.GameUI {
    public class UIItemViewArg : ValueReceiverBase {

        #region Public Fields

        [ReferenceType(typeof(object))] 
        public PropertyReference argSrc;

        #endregion

        #region ValueReceiverBase Implementation

        public override void UpdateReceiver() {
            GetComponents<IUIView>().ToList().ForEach(view => view.Show(argSrc.Get<object>()));
        }

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return argSrc;
        }
        
        #endregion
    }
}