using System;
using System.Collections;
using System.Linq;
using c1tr00z.AssistLib.Common.Coroutines;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public class MainCacher : MonoBehaviour {

        #region Class Implementation

        public CoroutineRequest Cache() {
            var request = new CoroutineRequest();
            StartCoroutine(C_Cache(request));
            return request;
        }

        private IEnumerator C_Cache(CoroutineRequest request) {
            var allCachers = ReflectionUtils.GetTypesByInterface(typeof(ICacher), false)
                .Select(t => Activator.CreateInstance(t) as ICacher).ToList();
            foreach (var cacher in allCachers) {
                Debug.Log($"Caching... {cacher.GetType()}");
                cacher.Cache();
                yield return null;
            }
            request.Finish();
        }

        #endregion
    }
}