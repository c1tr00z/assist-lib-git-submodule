using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class CameraFollower : MonoBehaviour {

        #region Public Fields

        [SerializeField] public Transform target;
        [SerializeField] public Vector3 offset;
        [SerializeField] public float speed;

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