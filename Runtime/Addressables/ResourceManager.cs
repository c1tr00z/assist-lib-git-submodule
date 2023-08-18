using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.Addressables {
    public sealed class ResourceManager : MonoBehaviour {

        #region Private Fields

        private static ResourceManager _instance;

        private Dictionary<string, Object> _assets = new();

        #endregion

        #region Accessors

        public static ResourceManager instance {
            get {
                if (_instance == null || _instance.gameObject == null || _instance.transform == null) {
                    _instance = new GameObject("SceneResourceManager").AddComponent<ResourceManager>();
                }

                return _instance;
            }
        }

        #endregion

        #region Class Implementation

        public bool TryGet<T>(AddressableReference reference, out T asset) {
            if (!_assets.ContainsKey(reference.address)) {
                asset = default;
                return false;
            }

            if (!_assets[reference.address] is T required) {
                asset = required;
                return true;
            }

            asset = default;
            return false;
        }

        public void SetAsset(AddressableReference reference, Object asset) {
            _assets[reference.address] = asset;
        }

        #endregion
    }
}