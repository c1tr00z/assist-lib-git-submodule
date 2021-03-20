
using System;

namespace c1tr00z.AssistLib.Utils {
    public static class DelegateUtils {

        #region Class Implementation

        public static void SafeInvoke(this Action action) {
            if (action.IsNull()) {
                return;
            }
            action.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> action, T param1) {
            if (action.IsNull()) {
                return;
            }
            action.Invoke(param1);
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2) {
            if (action.IsNull()) {
                return;
            }
            action.Invoke(param1, param2);
        }

        #endregion
    }
}
