using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.AppModules {
    public static class SubmodulesUtils {

        #region Private Fields

        private static Submodules _submodules;

        #endregion

        #region Accessors

        private static Submodules submodules => ModulesUtils.GetCachedModule(ref _submodules);

        #endregion
        
        #region Class Implementation

        public static T Get<T>() where T : SubmoduleBase {
            if (submodules == null) {
                return default;
            }

            return submodules.Get<T>();
        }

        public static T GetCached<T>(ref T submodule) where T : SubmoduleBase {
            if (submodule.IsNull()) {
                submodule = Get<T>();
            }

            return submodule;
        }

        #endregion
    }
}