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

    }
}