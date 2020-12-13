using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.Utils {

    public static class ComponentUtils {

        #region Class Implementation

        public static bool TryGetComponent<T>(this Component comp, out T targetComponent) {
            targetComponent = comp.GetComponent<T>();
            if (targetComponent != null) {
                return true;
            }

            return false;
        }

        public static T GetCachedComponent<T>(this Component comp, ref T cachedComponent) {
            if (cachedComponent == null) {
                cachedComponent = comp.GetComponent<T>();
            }

            return cachedComponent;
        }

        public static T GetCachedComponentInParent<T>(this Component comp, ref T cachedComponent) {
            if (cachedComponent == null) {
                cachedComponent = comp.GetComponentInParent<T>();
            }

            return cachedComponent;
        }

        public static T GetCachedComponentInChildren<T>(this Component comp, ref T cachedComponent) {
            if (cachedComponent == null) {
                cachedComponent = comp.GetComponentInChildren<T>();
            }

            return cachedComponent;
        }

        #endregion
    }
}
