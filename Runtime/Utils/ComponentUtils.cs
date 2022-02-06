using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.Utils {

    public static class ComponentUtils {

        #region Class Implementation

        /**
         * <summary>Tried to get component of type T from comp. If component is exist then returns true.</summary>
         */
        public static bool TryGetComponent<T>(this Component comp, out T targetComponent) {
            targetComponent = comp.GetComponent<T>();
            return !targetComponent.IsNull();
        }

        /**
         * <summary>Checks if cachedComponent not assigned then call GetComponent.
         * If cached component is assigned then just returns it</summary>
         */
        public static T GetCachedComponent<T>(this Component comp, ref T cachedComponent) {
            if (cachedComponent.IsNull()) {
                cachedComponent = comp.GetComponent<T>();
            }

            return cachedComponent;
        }

        /**
         * <summary>Checks if cachedComponents is null or empty then call GetComponents.
         * If cachedComponents not empty then just returns it</summary>
         */
        public static List<T> GetCachedComponents<T>(this Component comp, ref List<T> cachedComponents) {
            if (cachedComponents == null || cachedComponents.Count == 0) {
                cachedComponents = comp.GetComponents<T>().ToList();
            }

            return cachedComponents;
        }

        /**
         * <summary><see cref="GetCachedComponent{T}"/> for components in parent</summary>
         */
        public static T GetCachedComponentInParent<T>(this Component comp, ref T cachedComponent) {
            if (cachedComponent.IsNull()) {
                cachedComponent = comp.GetComponentInParent<T>();
            }

            return cachedComponent;
        }

        /**
         * <summary><see cref="GetCachedComponent{T}"/> for components in children</summary>
         */
        public static T GetCachedComponentInChildren<T>(this Component comp, ref T cachedComponent) {
            if (cachedComponent.IsNull()) {
                cachedComponent = comp.GetComponentInChildren<T>();
            }

            return cachedComponent;
        }

        /**
         * <summary><see cref="GetCachedComponents{T}"/> for components in children</summary>
         */
        public static List<T> GetCachedComponentsInChildren<T>(this Component comp, ref List<T> cachedComponents) {
            if (cachedComponents == null || cachedComponents.Count == 0) {
                cachedComponents = comp.GetComponentsInChildren<T>().ToList();
            }

            return cachedComponents;
        }

        #endregion
    }
}
