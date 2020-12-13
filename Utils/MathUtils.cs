using System;

namespace c1tr00z.AssistLib.Utils {
    public static class MathUtils {

        #region Class Implementation

        public static bool Even(this int x) {
            return (x & 1) == 0;
        }

        public static void DoTimes(this int times, Action<int> action) {
            for (int i = 0; i < times; i++) {
                action.SafeInvoke(i);
            }
        }

        #endregion
    }
}
