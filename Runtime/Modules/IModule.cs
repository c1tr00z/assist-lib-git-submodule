using c1tr00z.AssistLib.Common;
using Cysharp.Threading.Tasks;

namespace c1tr00z.AssistLib.AppModules {
    public interface IModule {

        #region Methods
        
        bool isInitialized { get; }

        UniTask InitializeModule();

        #endregion

    }
}