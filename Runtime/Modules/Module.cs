using Cysharp.Threading.Tasks;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class Module : MonoBehaviour, IModule {

        #region Accessors

        public bool isInitialized { get; private set; }

        #endregion

        #region Class Implementation

        public virtual UniTask InitializeModule() {
            isInitialized = true;
            return UniTask.CompletedTask;
        }

        #endregion
    }
}