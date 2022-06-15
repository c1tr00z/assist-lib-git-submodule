using c1tr00z.AssistLib.Common;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class Module : MonoBehaviour, IModule {

        #region Accessors

        public bool isInitialized { get; private set; }

        #endregion

        #region Class Implementation

        public virtual void InitializeModule(CoroutineRequest request) {
            isInitialized = true;
            request.Finish();
        }

        #endregion
    }
}