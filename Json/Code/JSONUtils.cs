using MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Json;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

public static class JSONUtuls {

    #region Class Implementation

    public static void Serialize(this IJsonSerializable serializable, Dictionary<string, object> json) {
        if (serializable == null) {
            return;
        }
        serializable.GetType().GetJsonSerializedFields().ForEach(f => {
            json.AddOrSet(f.Name, SerializeValue(f.GetValue(serializable)));
        });
        
        if (serializable is IJsonSerializableCustom) {
            ((IJsonSerializableCustom)serializable).SerializeCustom(json);
        }
    }

    public static void Deserialize(this IJsonDeserializable deserializable, Dictionary<string, object> json) {
        var fields = deserializable.GetType().GetJsonSerializedFields();
        
        json.Keys.ToList().ForEach(fieldName => {
            var field = fields.FirstOrDefault(f => f.Name == fieldName);

            if (field == null) {
                return;
            }

            var value = json[fieldName];

            field.SetValue(deserializable, DeserializeValue(value, field.FieldType));
        });
        
        if (deserializable is IJsonDeserializableCustom) {
            ((IJsonDeserializableCustom)deserializable).DeserializeCustom(json);
        }
    }

    private static object SerializeValue(object value) {

        if (value == null) {
            return null;
        }

        var valueType = value.GetType();

        object returnValue = value;

        if (valueType.GetInterfaces().Contains(typeof(IList<>))) {
            var listValue = new List<object>();

            var list = (IList)value;
            var enumerator = list.GetEnumerator();

            while (enumerator.MoveNext()) {
                listValue.Add(SerializeValue(enumerator.Current));
            }

            returnValue = listValue;

        } else if (value is IJsonSerializable serializable) {
            var valueJson = new Dictionary<string, object>();
            serializable.Serialize(valueJson);
            returnValue = valueJson;
        } else if (value is DBEntry dbEntry) {
            returnValue = dbEntry.name;
        }
        
        return returnValue;
    }

    private static object DeserializeValue(object value, Type targetType) {
        object returnValue = value;

        var isDeserializeToList = targetType.IsGenericType &&
                                  targetType.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IList<>));

        if (value == null && isDeserializeToList) {
            return Activator.CreateInstance(targetType);
        }

        if (value == null) {
            return null;
        }
        
        if (targetType.IsGenericType &&
            targetType.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IList))) {

            var listItemType = targetType.GetGenericArguments().FirstOrDefault();

            var list = (IList) value;

            var enumerator = list.GetEnumerator();

            var valueList = (IList)Activator.CreateInstance(targetType);

            while (enumerator.MoveNext()) {
                var listItemValue = enumerator.Current;
                var isDeserializable = listItemType.GetInterfaces().Contains(typeof(IJsonDeserializable));
                if (isDeserializable && listItemValue is Dictionary<string, object>) {
                    valueList.Add(DeserializeValue(listItemValue, listItemType));
                } else if (!isDeserializable) {
                    valueList.Add(listItemValue);
                }
            }

            returnValue = valueList;
        } else if (targetType.GetInterfaces().Contains(typeof(IJsonDeserializable))) {
            if (value is Dictionary<string, object> json) {
                var deserializable = Activator.CreateInstance(targetType);
                ((IJsonDeserializable)deserializable).Deserialize(json);
                returnValue = deserializable;
            }
        } else if (targetType == typeof(Int32) && value is Int64) {
            returnValue = (int) (long) value;
        } else if (targetType == typeof(Single) && value is Double) {
            returnValue = Convert.ToSingle((Double) value);
        } else if (value is string && typeof(DBEntry).IsAssignableFrom(targetType)) {
            
            returnValue = DB.Get<DBEntry>(value.ToString());
            
        } else if (value is string && typeof(Vector2).IsAssignableFrom(targetType)) {
            
            if (VectorUtils.TryParse(value.ToString(), out Vector2 vector)) {
                returnValue = vector;
            }
            
        } else if (value is string && typeof(Vector3).IsAssignableFrom(targetType)) {
            
            if (VectorUtils.TryParse(value.ToString(), out Vector3 vector)) {
                returnValue = vector;
            }
            
        } else if (value is string && typeof(Vector4).IsAssignableFrom(targetType)) {
            
            if (VectorUtils.TryParse(value.ToString(), out Vector4 vector)) {
                returnValue = vector;
            } 
        }

        // if (!targetType.IsInstanceOfType(returnValue)) {
        //     Debug.LogError("b: " + returnValue + " \\ type : " + returnValue.GetType() + " \\ target: " + targetType);
        // }


        return returnValue;
    }

    public static string ToJsonString(this IJsonSerializable serializable) {
        return Serialize(ToJson(serializable));
    }

    public static Dictionary<string, object> ToJson(this IJsonSerializable serializable) {
        var json = new Dictionary<string, object>();
        serializable.Serialize(json);
        return json;
    }

    public static T FromJsonString<T>(string jsonString) where T : IJsonDeserializable {
        var json = Deserialize(jsonString);
        var deserializable = Activator.CreateInstance<T>();
        deserializable.Deserialize(json);
        return deserializable;
    }

    public static string Serialize(object objectToSerialize) {
        return Json.Serialize(objectToSerialize);
    }

    public static Dictionary<string, object> Deserialize(string jsonString) {
        var deserialized = Json.Deserialize(jsonString);

        if (deserialized == null) {
            Debug.LogError("JSON string is corrupted!");
            Debug.LogError("Corrupted json: " + jsonString);
            return new Dictionary<string, object>();
        }

        return (Dictionary<string, object>)deserialized;
    }

    public static T Deserialize<T>(Dictionary<string, object> json) where T : IJsonDeserializable {
        var deserializable = Activator.CreateInstance<T>();
        deserializable.Deserialize(json);
        return deserializable;
    }

    public static Dictionary<string, object> GetChild(this Dictionary<string, object> json, string key) {
        return json.ContainsKey(key) ? (Dictionary<string, object>)json[key] : new Dictionary<string, object>();
    }

    #endregion
}
