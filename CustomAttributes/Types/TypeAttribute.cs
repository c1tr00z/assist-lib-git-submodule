using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.CustomAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeAttribute : PropertyAttribute {

        public Type baseType { get; set; }

        public TypeAttribute() {
        }

        public TypeAttribute(Type baseType) {
            this.baseType = baseType;
        }
    }
}
