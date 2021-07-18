using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
    public static class PropertyReferenceUtils {

        #region Private Fields

        private static Dictionary<string, PropertyInfo> _properties = new Dictionary<string, PropertyInfo>();

        #endregion

        #region Class Implementation

        public static T Get<T>(this PropertyReference propertyReference) {
            if (propertyReference.getter == null) {

                var propertyInfo = propertyReference.GetPropertyInfo();
                
                propertyReference.getter = PropertyValueGetterUtils.MakeGetter(propertyInfo.PropertyType, propertyReference.target, propertyInfo);
            }
            
            if (propertyReference.getter == null) {
                return default(T);
            }

            var value = propertyReference.GetValue();
            if (typeof(T) == typeof(string)) {
                if (value == null) {
                    return default(T);
                }
                return (T)(object)value.ToString();
            }
            if (value is T) {
                return (T)value;
            }
            return default(T);
        }

        public static PropertyInfo GetPropertyInfo(this PropertyReference propertyReference) {
            var key = propertyReference.GetPropertyKey();
                
            if (!_properties.ContainsKey(key)) {
                var type = propertyReference.target.GetType();
                _properties.AddOrSet(key, type.GetPublicPropertyInfo(propertyReference.fieldName));
            }

            return _properties[key];
        }

        public static List<T> GetList<T>(this PropertyReference propertyReference) {
            var iList = (IList)propertyReference.Get<object>();
            if (iList == null) {
                return new List<T>();
            }
            var list = new List<T>();
            var listEnum = iList.GetEnumerator();
            while (listEnum.MoveNext()) {
                var newItem = (T)listEnum.Current;
                if (newItem != null) {
                    list.Add(newItem);
                }
            }

            return list;
        }

        private static string GetPropertyKey(this PropertyReference propertyReference) {
            return $"{propertyReference.target.GetType().FullName}.{propertyReference.fieldName}";
        }

        public static object GetValue(this PropertyReference propertyReference) {
            return propertyReference.getter.GetValue();
        }

        public static bool IsValid(this PropertyReference propertyReference) {
            return propertyReference.target != null && !string.IsNullOrEmpty(propertyReference.fieldName);
        }

        #endregion
    }
}