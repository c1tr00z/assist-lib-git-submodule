using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public struct TransformData {

        #region Public Fields

        public Vector3 position;

        public Quaternion rotation;

        public Vector3 scale;

        #endregion

        #region Accessors

        public bool isDefault => position == Vector3.zero && rotation == Quaternion.identity && scale == Vector3.one;

        public static TransformData identity => new TransformData {
            position = Vector3.zero,
            rotation = Quaternion.identity,
            scale = Vector3.one
        };

        #endregion

        #region Object Implementation

        public override string ToString() {
            return $"P:{position}, R:{rotation}, S:{scale}";
        }

        #endregion

        #region Class Implementation

        public static TransformData MakeFromPosition(Vector3 position) {
            return new TransformData {
                position = position,
                rotation = Quaternion.identity,
                scale = Vector3.one,
            };
        }
        
        public static TransformData MakeFromPositionAndRotation(Vector3 position, Quaternion rotation) {
            return new TransformData {
                position = position,
                rotation = rotation,
                scale = Vector3.one,
            };
        }

        public static TransformData MakeFromTransform(Transform transform) {
            return new TransformData {
                position = transform.position,
                rotation = transform.rotation,
                scale = transform.localScale
            };
        }

        #endregion
    }
}