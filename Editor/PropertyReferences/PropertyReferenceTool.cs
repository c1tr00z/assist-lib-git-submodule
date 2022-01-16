using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using c1tr00z.AssistLib.EditorTools;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences.Editor {
    [EditorToolName("Property References")]
    public class PropertyReferenceTool : EditorTool {

        #region EditorTool Implementation

        public override void DrawInterface() {
            if (Button("Generate class")) {
                Scan();
            }
        }

        #endregion

        #region Class Implementation

        private void Scan() {
            var allPaths = AssetDatabase.GetAllAssetPaths().ToList();
            
            var types = new List<Type>();
            allPaths.ForEach(p => {
                types.AddRange(ScanPath(p));
            });

            types = types.ToUniqueList();
            
            GenerateClass(types);
        }

        private List<Type> ScanPath(string path) {

            var types = new List<Type>();
            
            if (path.ToLower().EndsWith(".prefab")) {
                types.AddRange(ScanPrefab(path));
            } else if (path.ToLower().EndsWith(".asset")) {
                ScanScriptableObject(path);
            }

            return types;
        }

        private List<Type> ScanPrefab(string path) {
            var types = new List<Type>();
            var assets = AssetDatabase.LoadAllAssetsAtPath(path).SelectNotNull();
            assets.ForEach(a => {

                var allFittedFields = a.GetType().GetFields(BindingFlags.Default | BindingFlags.Public |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.Instance);
                
                var propRefFields = allFittedFields.Where(f => f.FieldType == typeof(PropertyReference)).ToList();

                propRefFields.ForEach(f => {
                    var propReference = (PropertyReference)f.GetValue(a);
                    
                    var referenceTypeAttribute = f.GetCustomAttributes().OfType<ReferenceTypeAttribute>().FirstOrDefault();

                    if (CachePropertyReference(propReference, referenceTypeAttribute, out List<Type> foundedTypes)) {
                        types.AddRange(foundedTypes);
                    }
                });
                
                var propRefEnumerableFields = new List<FieldInfo>();
                propRefEnumerableFields.AddRange(allFittedFields.Where(f => f.FieldType == typeof(PropertyReference[])));
                propRefEnumerableFields.AddRange(allFittedFields.Where(f => f.FieldType == typeof(List<PropertyReference>)));
                
                
                propRefEnumerableFields.ForEach(f => {
                    
                    var referenceTypeAttribute = f.GetCustomAttributes().OfType<ReferenceTypeAttribute>().FirstOrDefault();

                    var propRefArray = (IEnumerable<PropertyReference>)f.GetValue(a);

                    if (propRefArray == null) {
                        return;
                    }
                    
                    propRefArray.ToList().ForEach(propReference => {
                        if (CachePropertyReference(propReference, referenceTypeAttribute, out List<Type> foundedTypes)) {
                            types.AddRange(foundedTypes);
                        }
                    });
                });
            });

            return types;
        }

        private bool CachePropertyReference(PropertyReference propertyReference, ReferenceTypeAttribute referenceTypeAttribute, out List<Type> types) {
            
            types = new List<Type>();
            
            if (propertyReference == null || !propertyReference.IsValid()) {
                return false;
            }
            
            var property = propertyReference.GetPropertyInfo();
            
            types.Add(property.PropertyType);

            if (referenceTypeAttribute == null) {
                types.Add(typeof(object));
                return true;
            }

            var target = propertyReference.target;

            if (!referenceTypeAttribute.type.IsGenericType) {
                types.Add(referenceTypeAttribute.type);
                if (property.PropertyType != referenceTypeAttribute.type) {
                    types.Add(property.PropertyType);
                }
            }

            var propertyInfo = ReflectionUtils.GetPublicPropertyInfo(target.GetType(), propertyReference.fieldName);
            types.Add(propertyInfo.PropertyType);

            return true;
        }

        private void ScanScriptableObject(string path) {
            
        }

        private void GenerateClass(List<Type> types) {

            var classText = "//This is auto generated class\r\n";

            classText += MakeLine($"public class il_2_cpp_Cache : {typeof(PropertyReferenceCacher).FullName} {{", 0);

            classText += MakeLine("public override void Cache() {", 1);
            
            classText += MakeLine("", 2);
            
            types.ForEach(t => {
                classText += MakeLine($"Cache<{t.GetValidFullName()}>();", 2);
            });
            
            classText += MakeLine("", 2);
            
            classText += MakeLine("}", 1);
            
            classText += MakeLine("}", 0);

            SaveToFile(classText);
        }

        private string MakeLine(string lineText, int indents) {
            indents = indents >= 0 ? indents : 0;

            var tabs = "";
            for (int i = 0; i < indents; i++) {
                tabs += "\t";
            }

            return $"{tabs}{lineText}\r\n";
        }

        private void SaveToFile(string text) {
            var directoryPath = Path.Combine(Application.dataPath, "Common", "Code");
            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists) {
                directory.Create();
            }

            var filePath = Path.Combine(directoryPath, "il_2_cpp_Cache.cs");
            var file = new FileInfo(filePath);
            if (file.Exists) {
                file.Delete();
            }
            
            using (var writer = file.CreateText()) {
                writer.WriteLine(text);
                writer.Flush();
                writer.Close();
            }
            
            AssetDatabase.Refresh();
        }

        #endregion
    }
}