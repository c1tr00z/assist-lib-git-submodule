using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class CameraFollower : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _speed;

        #endregion

        #region Unity Events

        private void LateUpdate() {

            if (!_target.IsAssigned()) {
                return;
            };
            
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset,
                Time.deltaTime * _speed);
        }

        #endregion
    }
}