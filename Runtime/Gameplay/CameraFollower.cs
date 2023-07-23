using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Gameplay {
    public class CameraFollower : MonoBehaviour {

        #region Public Fields

        public List<Transform> targets = new();
        public Vector3 offset;
        public float speed;

        public bool trackForward;

        public float forwardShift;

        #endregion

        #region Unity Events

        private void LateUpdate() {

            if (targets.Count == 0) {
                return;
            }

            var targetPosition = VectorUtils.Average(targets.SelectNotNull(t => t.position).ToArray()) + offset;
            
            if (trackForward && targets.Count == 1) {
                var target = targets.FirstOrDefault();
                targetPosition += target.forward.normalized * forwardShift;
            }
            
            transform.position = Vector3.Lerp(transform.position, targetPosition,
                Time.deltaTime * speed);
        }

        #endregion

        #region Class Implementation

        public void ClearTargets() {
            targets.Clear();
        }

        public void AddTarget(Transform target) {
            targets.Add(target);
        }

        #endregion
    }
}