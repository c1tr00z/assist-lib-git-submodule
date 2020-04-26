using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionUtils {

    private static List<Type> _types = new List<Type>();

    public static List<Type> GetTypes() {
        if (_types == null || _types.Count == 0) {
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            assemblies.ForEach(assembly => {
                _types.AddRange(assembly.GetTypes());
            });
        }
        return _types;
    }

	public static Type GetTypeByName(string name) {
		return GetTypes().FirstOrDefault(t => t.FullName == name);	
	}

    public static List<Type> GetSubclassesOf(Type baseClass) {
        if (baseClass == null) {
            return GetTypes();
        }
        return GetTypes().Where(t => t.IsSubclassOf(baseClass)).ToList();
    }

    public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type) {
        return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
    }

	public static PropertyInfo GetPublicPropertyInfo(this Type type, string fieldName) {
		return type.GetProperty(fieldName, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
	}

	public static string GetPropertyNameByType(this PropertyInfo propertyInfo) {
		return $"{propertyInfo.PropertyType.Name}/{propertyInfo.Name}";
	}
}
