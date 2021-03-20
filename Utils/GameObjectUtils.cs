using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.Utils {
    public static class GameObjectUtils {

        #region Class Implementation

        public static void Destroy(this GameObject gameObject) {
            if (gameObject.IsNull()) {
                return;
            }
#if UNITY_EDITOR
            Object.DestroyImmediate(gameObject);
#else
            Object.Destroy(gameObject);
#endif
        }

        public static void SetLayer(this GameObject gameObject, int layer, bool includeChildren = true) {
            gameObject.layer = layer;
            if (includeChildren) {
                gameObject.transform.GetChildren().SelectNotNull(c => c.gameObject).ToList()
                    .ForEach(go => go.SetLayer(layer, true));
            }
        }

        #endregion
    }
}