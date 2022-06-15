using c1tr00z.AssistLib.Common;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    public class AssetRequest<T> : RequestBase where T : Object {

        #region Accessors

        public T asset { get; private set; }

        #endregion

        #region Class Implementation

        public void AssetLoaded(T asset) {
            if (state == State.Done) {
                return;
            }
            this.asset = asset;
            Finish();
        }

        #endregion
    }
}