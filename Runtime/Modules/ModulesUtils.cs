namespace c1tr00z.AssistLib.AppModules {
    public static class ModulesUtils {

        #region Class Implementation

        public static T GetCachedModule<T>(ref T module) where T : Module {
            if (module == null) {
                module = Modules.Get<T>();
            }

            return module;
        }

        #endregion
    }
}