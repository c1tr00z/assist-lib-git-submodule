using System;
using System.Collections;
using UnityEngine;

namespace c1tr00z.AssistLib.Addressables {
    public class AddressableTest : MonoBehaviour {
        [AddressableType(typeof(GameObject))]
        public AddressableReference randomAddressableTest;

        private IEnumerator Start() {
            yield return null;
        }
    }
}