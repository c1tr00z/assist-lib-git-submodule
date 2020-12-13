using UnityEngine;
using System.Collections;

namespace c1tr00z.AssistLib.Utils {
    public static class ObjectUtils {

        #region Class Implementation

        public static T Clone<T>(this T original, Vector3 position, Quaternion rotation, Transform parent)
            where T : Object {
            return Object.Instantiate(original, position, rotation, parent) as T;
        }

        public static T Clone<T>(this T obj) where T : Object {
            return Clone(obj, null);
        }

        public static T Clone<T>(this T obj, Transform parent) where T : Object {
            return Clone(obj, Vector3.zero, Quaternion.identity, parent);
        }

        public static T Clone<T>(this T obj, Vector3 position, Transform parent) where T : Object {
            return Clone(obj, position, Quaternion.identity, parent);
        }

        /**
         * <summary>Resets local position, rotation and scale</summary>
         */
        public static void Reset<T>(this T obj) where T : Component {
            obj.Reset(null);
        }

        public static void Reset<T>(this T obj, Transform parent) where T : Component {
            if (parent != null) {
                obj.transform.SetParent(parent, false);
            }

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            obj.transform.localRotation = Quaternion.identity;
        }

        public static bool IsAssigned(this object obj) {
            if (obj == null) {
                return false;
            }

            if (obj is GameObject gameObject) {
                return gameObject != null && !"null".Equals(gameObject) && !"Null".Equals(gameObject);
            }

            if (obj is Component component) {
                return component != null && component.gameObject.IsAssigned() && component.transform != null &&
                       !"null".Equals(component) && !"Null".Equals(component);
            }

            return obj != null;
        }

        #endregion
    }
}
