using System.Collections;
using c1tr00z.AssistLib.Common.Coroutines;

namespace c1tr00z.AssistLib.AppModules {
    public interface IModule {

        #region Methods

        void InitializeModule(CoroutineRequest request);

        #endregion

    }
}