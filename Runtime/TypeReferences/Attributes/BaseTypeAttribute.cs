using System;
using UnityEngine;

namespace c1tr00z.AssistLib.TypeReferences {

    public class BaseTypeAttribute : PropertyAttribute {

        #region Accessors

        public Type type { get; private set; }
        
        public bool includeBaseClass { get; private set; }

        #endregion

        #region Constructors

        public BaseTypeAttribute (Type type, bool includeBaseClass = false) {
            this.type = type;
            this.includeBaseClass = includeBaseClass;
        }

        #endregion

        #region Attributes

        public override bool Match (object obj) {
            var other = obj as BaseTypeAttribute;
            if (other == null) {
                return false;
            }

            return other.type == this.type;
        }

        #endregion
    }
}
