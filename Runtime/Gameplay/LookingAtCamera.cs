using System;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class LookingAtCamera : MonoBehaviour {

        #region Serialized Fields

        [SerializeField]
        private Camera _camera;

        [SerializeField] private bool _selectMainCamera = true;

        [SerializeField] private bool _useAngleX = true;
        [SerializeField] private bool _useAngleY = true;
        [SerializeField] private bool _useAngleZ = true;

        #endregion

        #region Accessors

        private Camera cameraToLookAt =>
            CommonUtils.GetCachedObject(ref _camera, () => _selectMainCamera ? Camera.main : null);

        private bool isCameraSet => !cameraToLookAt.IsNull();

        private Transform cameraTransform => cameraToLookAt?.transform;

        #endregion

        #region Unity Events

        private void LateUpdate() {
            if (!isCameraSet) {
                return;
            }

            var newRotation = Quaternion.LookRotation(transform.GetDirection(cameraTransform));
            if (!_useAngleX || !_useAngleY || !_useAngleZ) {
                var angles = newRotation.eulerAngles;
                var currentAngles = transform.rotation.eulerAngles;
                newRotation.eulerAngles = new Vector3(
                    _useAngleX ? angles.x : currentAngles.x,
                    _useAngleY ? angles.y : currentAngles.y,
                    _useAngleZ ? angles.z : currentAngles.z);
            }

            transform.rotation = newRotation;
        }

        #endregion
    }
}