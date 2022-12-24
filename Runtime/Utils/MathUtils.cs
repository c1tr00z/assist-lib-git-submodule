using System;
using UnityEngine;

namespace c1tr00z.AssistLib.Utils {
    public static class MathUtils {

        #region Class Implementation

        public static bool Even(this int x) {
            return (x & 1) == 0;
        }

        public static void DoTimes(this int times, Action<int> action) {
            for (int i = 0; i < times; i++) {
                action?.Invoke(i);
            }
        }

        public static bool FastApproximately(float a, float b, float threshold) {
            if (threshold == 0) {
                return Mathf.Approximately(a, b);
            }

            return (a - b < 0 ? (a - b) * -1 : a - b) <= threshold;
        }

        #endregion
    }
}
