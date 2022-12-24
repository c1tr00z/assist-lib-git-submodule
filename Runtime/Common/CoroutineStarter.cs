using System.Collections;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public class CoroutineStarter : MonoBehaviour {

        #region Private Fields

        private static CoroutineStarter _instance;

        #endregion
        
        #region Accessors

        private static CoroutineStarter instance {
            get {
                if (_instance == null) {
                    _instance = new GameObject("CoroutineStarter").AddComponent<CoroutineStarter>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        #endregion

        #region Class Implementation

        public static Coroutine RequestCoroutine(IEnumerator coroutine) {
            return instance.StartCoroutine(coroutine);
        }

        #endregion

    }
}