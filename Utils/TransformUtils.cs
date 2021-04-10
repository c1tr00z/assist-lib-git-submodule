using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace c1tr00z.AssistLib.Utils {
    public static class TransformUtils {

        #region Class Implementations

        public static List<Transform> GetChildren(this Transform transform) {
            var list = new List<Transform>();
            for (var i = 0; i < transform.childCount; i++) {
                list.Add(transform.GetChild(i));
            }
            return list;
        }

        public static void SetChildrenSiblingIndex(this Transform transform, Func<Transform, int> siblingIndexSelector) {
            GetChildren(transform).ForEach(c => c.SetSiblingIndex(siblingIndexSelector(c)));
        }

        public static Vector2 GetUIScreenPosition(this Transform transform) {
            var scaler = transform.GetComponentInParent<CanvasScaler>();
            var camera = Camera.allCameras.FirstOrDefault(c => (c.cullingMask & (1 << 5)) == 0);

            var cameraPosition = camera.WorldToScreenPoint(transform.position).ToVector2();
            var scale = scaler.transform.localScale.ToVector2();

            return new Vector2(cameraPosition.x / scale.x, cameraPosition.y / scale.y);
        }

        public static void DestroyAllChildren(this Transform transform) {
            var listDestroy = transform.GetChildren().ToList();
            listDestroy.ForEach(t => t.gameObject.Destroy());
        }
    
        public static Vector3 GetHeading(this Transform transform, Transform to) {

            if (to == null) {
                return Vector3.zero;
            }
        
            var targetPosition = to.position;
            var position = transform.position;

            return targetPosition - position;
        }

        public static float GetDistance(this Transform transform, Transform to) {
            var heading = transform.GetHeading(to);

            return heading.magnitude;
        }

        public static Vector3 GetDirection(this Transform transform, Transform to) {
        
            var heading = transform.GetHeading(to);
            var distance = heading.magnitude;

            return heading / distance;
        }

        public static void PlaceTo(this Transform transform, Transform other) {
            if (transform.IsNull() || other.IsNull()) {
                return;
            }

            transform.position = other.position;
            transform.rotation = other.rotation;
        }

        #endregion
    }
}
