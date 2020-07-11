using System;

namespace c1tr00z.AssistLib.Utils {
    public static class CommonUtils {
        public static T GetCachedObject<T>(ref T obj, Func<T> getter) {
            if (obj == null && getter != null) {
                obj = getter != null ? getter() : default(T);
            }

            return obj;
        }
    }
}