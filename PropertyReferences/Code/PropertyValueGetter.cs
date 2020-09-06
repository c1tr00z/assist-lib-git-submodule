using System;
using System.Reflection;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
    public abstract class PropertyValueGetter {

        public abstract Type GetGenericType();
        
        public abstract object GetValue();

        public abstract void Init(object target, PropertyInfo propertyInfo);
    }

    public class PropertyValueGetter<T> : PropertyValueGetter {

        public Func<T> getter;

        public override Type GetGenericType() {
            return typeof(T);
        }

        public override object GetValue() {
            return getter();
        }

        public override void Init(object target, PropertyInfo propertyInfo) {
            var methodInfo = propertyInfo.GetGetMethod();
            
            var methodType = typeof(Func<>).MakeGenericType(propertyInfo.PropertyType);
            
            getter = (Func<T>)Delegate.CreateDelegate(methodType, target, methodInfo, true);
        }
    }
}