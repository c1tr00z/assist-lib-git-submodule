using System;

namespace c1tr00z.AssistLib.Utils {
    public static class CommonUtils {

        #region Class Implementation

        public static T GetCachedObject<T>(ref T obj, Func<T> getter) {
            if (obj.IsNull()) {
                obj = getter != null ? getter() : default;
            }

            return obj;
        }

        #endregion
    }
}