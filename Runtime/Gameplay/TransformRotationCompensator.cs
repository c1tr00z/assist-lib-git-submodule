using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class TransformRotationCompensator : MonoBehaviour {

        #region Private Fields

        private Transform _transform;

        #endregion

        #region Serialized Fields

        [SerializeField]
        private Transform _transformToCompensate;

        #endregion

        #region Accessors

        public Transform myTransform => this.GetCachedComponent(ref _transform);

        #endregion

        #region Unity Events

        private void LateUpdate() {
            if (_transformToCompensate.IsNull()) {
                return;
            }
            myTransform.localRotation = Quaternion.Inverse(_transformToCompensate.rotation);
        }

        #endregion
    }
}