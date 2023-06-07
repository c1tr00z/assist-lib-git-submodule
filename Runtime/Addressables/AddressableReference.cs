using System;

namespace c1tr00z.AssistLib.Addressables {
    [Serializable]
    public struct AddressableReference {

        #region Public Fields

        public string guid;

        public string address;

        #endregion

        #region Accessors

        public string key => address;

        #endregion

    }
}