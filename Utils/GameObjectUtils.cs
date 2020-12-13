using UnityEngine;

namespace c1tr00z.AssistLib.Utils {
    public static class GameObjectUtils {

        #region Class Implementation

        public static void Destroy(this GameObject gameObject) {
            if (!gameObject.IsAssigned()) {
                return;
            }
#if UNITY_EDITOR
            Object.DestroyImmediate(gameObject);
#else
            Object.Destroy(gameObject);
#endif
        }

        #endregion
    }
}