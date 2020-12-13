using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class CameraFollower : MonoBehaviour {

        #region Public Fields

        public Transform target;
        public Vector3 offset;
        public float speed;

        #endregion

        #region Unity Events

        private void LateUpdate() {

            if (!target.IsAssigned()) {
                return;
            };
            
            transform.position = Vector3.Lerp(transform.position, target.position + offset,
                Time.deltaTime * speed);
        }

        #endregion
    }
}