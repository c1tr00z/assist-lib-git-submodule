using System;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    public class DBEntryTypeAttribute : PropertyAttribute {

        #region Accessors

        public Type type { get; }

        #endregion

        #region Constructors

        public DBEntryTypeAttribute(Type type) {
            this.type = type;
        }

        #endregion
    }
}