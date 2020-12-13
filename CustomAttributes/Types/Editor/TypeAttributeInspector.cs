using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.CustomAttributes {
    [CustomPropertyDrawer(typeof(TypeAttribute))]
    public class TypeAttributeInspector : ExtendedPropertyDrawer {

        private List<Type> _selectedTypes = new List<Type>();

        private TypeAttribute typeAttribute => attribute as TypeAttribute;

        public TypeAttributeInspector() {
            
        }

        public override void Show(SerializedProperty property) {

            if (typeAttribute == null) {
                GUI.Label(position, new GUIContent("No attribute"));
                return;
            }

            if (_selectedTypes == null || _selectedTypes.Count == 0) {
                _selectedTypes = ReflectionUtils.GetSubclassesOf(typeAttribute.baseType);
            }
        }
    }
}

