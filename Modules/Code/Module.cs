using c1tr00z.AssistLib.Common.Coroutines;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class Module : MonoBehaviour, IModule {

        #region Class Implementation

        public virtual CoroutineRequest InitializeModule() {
            return CoroutineRequest.MakeFinishedRequest();
        }

        #endregion
    }
}