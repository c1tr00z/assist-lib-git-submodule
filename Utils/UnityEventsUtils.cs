using UnityEngine.Events;

namespace AssistLib.Utils {
    public static class UnityEventsUtils {

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
    }
}