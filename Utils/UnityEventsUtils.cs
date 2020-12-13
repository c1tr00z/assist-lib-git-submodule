using UnityEngine.Events;

namespace c1tr00z.AssistLib.Utils {
    public static class UnityEventsUtils {

        #region Class Implementations

        public static void SafeInvoke(this UnityEvent unityEvent) {
            if (!unityEvent.IsAssigned()) {
                return;
            }
            unityEvent?.Invoke();
        }
        
        public static void SafeInvoke<T>(this UnityEvent<T> unityEvent, T param) {
            if (!unityEvent.IsAssigned()) {
                return;
            }
            unityEvent?.Invoke(param);
        }

        #endregion
    }
}