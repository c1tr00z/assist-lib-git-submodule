using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class CameraFollower : MonoBehaviour {

        #region Public Fields

        public Transform target;
        public Vector3 offset;
        public float speed;

        public bool trackForward;

        public float forwardShift;

        #endregion

        #region Unity Events

        private void LateUpdate() {

            if (!target.IsAssigned()) {
                return;
            }

            var targetPosition = target.position + offset;
            
            if (trackForward) {
                targetPosition += target.forward.normalized * forwardShift;
            }
            
            transform.position = Vector3.Lerp(transform.position, targetPosition,
                Time.deltaTime * speed);
        }

        #endregion
    }
}