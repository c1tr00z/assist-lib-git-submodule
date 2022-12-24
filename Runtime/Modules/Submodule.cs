namespace c1tr00z.AssistLib.AppModules {
    public abstract class Submodule<T> : SubmoduleBase where T : Module {

        #region Private Fields

        private T _module;

        #endregion

        #region Accessors

        protected T module => ModulesUtils.GetCachedModule(ref _module);

        #endregion

        #region SubmoduleBase Implementation

        public override void Init(Module module) {
            if (!(module is T realModule)) {
                return;
            }
            
            InitSubmodule(realModule);
        }

        #endregion

        #region Class Implementation

        public virtual void InitSubmodule(T newModule) {
            _module = newModule;
        }

        #endregion
    }
}