using System;
using System.Reflection;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
    public abstract class PropertyValueGetter {

        #region Abstract Methods

        public abstract Type GetGenericType();
        
        public abstract object GetValue();

        public abstract void Init(object target, PropertyInfo propertyInfo);

        #endregion
    }

    public class PropertyValueGetter<T> : PropertyValueGetter {

        #region Public Fields

        public Func<T> getter;

        #endregion

        #region Class Implementation

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

        #endregion
    }
}