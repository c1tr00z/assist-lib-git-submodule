using System;
using UnityEngine;

namespace c1tr00z.AssistLib.Addressables {
    public class AddressableTypeAttribute : PropertyAttribute {

        #region Accessors

        public Type assetType { get; }

        #endregion

        #region Contructors

        public AddressableTypeAttribute(Type type) {
            assetType = type;
        }

        #endregion
    }
}