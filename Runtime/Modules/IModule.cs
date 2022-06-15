using c1tr00z.AssistLib.Common;

namespace c1tr00z.AssistLib.AppModules {
    public interface IModule {

        #region Methods
        
        bool isInitialized { get; }

        void InitializeModule(CoroutineRequest request);

        #endregion

    }
}