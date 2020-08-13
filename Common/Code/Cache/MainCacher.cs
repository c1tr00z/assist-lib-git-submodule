using System;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public class MainCacher : MonoBehaviour {

        #region Unity Events

        private void Awake() {
            Cache();
        }

        #endregion

        #region Class Implementation

        private void Cache() {
            var allCachers = ReflectionUtils.GetTypesByInterface(typeof(ICacher))
                .Select(t => Activator.CreateInstance(t) as ICacher);
            allCachers.ForEach(c => c.Cache());
        }

        #endregion
    }
}