using UnityEngine;
using System.Collections;
using c1tr00z.AssistLib.Common;

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

        public static T Clone<T>(this T obj, TransformData transformData) where T : Object {
            return Clone(obj, transformData.position, transformData.rotation, null);
        }

        /**
         * <summary>Resets local position, rotation and scale</summary>
         */
        public static void Reset<T>(this T obj) where T : Component {
            obj.Reset(null);
        }

        /**
         * <summary>Resets local position, rotation and scale. If parent is not null then puts transform in parent
         * and then reset parameters</summary>
         */
        public static void Reset<T>(this T obj, Transform parent) where T : Component {
            obj.transform.SetParent(parent, false);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            obj.transform.localRotation = Quaternion.identity;
        }

        /**
         * Checks if object is assigned. If obj is Unity Object then checks if gameObject is alive
         * (avoiding serialized object problem)
         */
        public static bool IsNull(this object obj) {
            if (obj == null) {
                return true;
            }

            if (obj is GameObject gameObject) {
                return gameObject == null || "null".Equals(gameObject) || "Null".Equals(gameObject);
            }

            if (obj is Component component) {
                return component == null || component.gameObject.IsNull() || component.transform == null ||
                       "null".Equals(component) || "Null".Equals(component);
            }

            return obj == null;
        }

        #endregion
    }
}
