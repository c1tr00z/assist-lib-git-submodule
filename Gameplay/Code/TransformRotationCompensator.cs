using System;
using UnityEngine;

namespace AssistLib.Gameplay.Code {
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
            if (!_transformToCompensate.IsAssigned()) {
                return;
            }
            myTransform.localRotation = Quaternion.Inverse(_transformToCompensate.rotation);
        }

        #endregion
    }
}