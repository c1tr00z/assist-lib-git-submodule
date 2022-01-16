using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
    public static class PropertyValueGetterUtils {

        #region Private Fields

        private static Dictionary<Type, ValueGetterCache> _getters = new Dictionary<Type, ValueGetterCache>();

        #endregion

        #region Class Implementation

        public static void AddTypeByGetter(ValueGetterCache getterCache) {
            if (_getters.ContainsKey(getterCache.genericType)) {
                return;
            }
            
            _getters.Add(getterCache.genericType, getterCache);
        }

        public static ValueGetterCache GetGetterCache<T>() {
            return GetGetterCache(typeof(T));
        }

        public static ValueGetterCache GetGetterCache(Type type) {
            if (!_getters.ContainsKey(type)) {
                throw new UnityException($"Cache error. No cached type {type}");
            }

            return _getters[type];
        }

        public static PropertyValueGetter MakeGetter(Type genericType, object target, PropertyInfo propertyInfo) {
            var getterCache = GetGetterCache(genericType);

            var getter = getterCache.MakeValueGetter();
            
            getter.Init(target, propertyInfo);

            return getter;
        }

        public static PropertyValueGetter MakeGetter<T>(object target, PropertyInfo propertyInfo) {
            return  MakeGetter(typeof(T), target, propertyInfo);
        }

        #endregion
    }
}