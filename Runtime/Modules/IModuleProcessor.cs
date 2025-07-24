using Cysharp.Threading.Tasks;

namespace c1tr00z.AssistLib.AppModules {
    public interface IModuleProcessor {
        #region Methods

        public UniTask PreInitProcess(Module module);

        #endregion
    }
}