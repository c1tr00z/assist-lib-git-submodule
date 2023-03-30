using System;
using System.Collections;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.Utils;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace c1tr00z.AssistLib.Addressables {
    public static class AddressableUtils {

        #region Class Implementation

        public static void LoadIResourceLocation(this AddressableReference reference,
            Action<IResourceLocation> callback) {

            CoroutineStarter.RequestCoroutine(C_LoadIResourceLocation(reference, callback));
        }

        private static IEnumerator C_LoadIResourceLocation(AddressableReference reference,
            Action<IResourceLocation> callback) {

            var handle = UnityEngine.AddressableAssets.Addressables.LoadResourceLocationsAsync(reference.key);

            yield return handle;
            
            callback?.Invoke(handle.Result.FirstOrDefault());
        }

        public static AddressableReference MakeFromAddress(string address) {
            return new AddressableReference {
                address = address
            };
        }

        #endregion
    }
}