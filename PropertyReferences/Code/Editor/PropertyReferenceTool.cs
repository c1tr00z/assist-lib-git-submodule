using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using c1tr00z.AssistLib.EditorTools;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences.Editor {
    public class PropertyReferenceTool : EditorTool {

        protected override void DrawInterface() {
            base.DrawInterface();

            if (Button("Generate class")) {
                Scan();
            }
        }

        private void Scan() {
            var allPaths = AssetDatabase.GetAllAssetPaths();
            
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
            var assets = AssetDatabase.LoadAllAssetsAtPath(path);
            assets.ForEach(a => {
                
                var propRefFields = a.GetType().
                    GetFields(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic |
                                      BindingFlags.Instance).Where(f => f.FieldType == typeof(PropertyReference)).ToList();

                propRefFields.ForEach(f => {
                    var propReference = (PropertyReference)f.GetValue(a);
                    if (propReference == null || !propReference.IsValid()) {
                        Debug.LogError($"Invalid PropertyReference in {a.name} (type: {a.GetType()}, fieldName: {f.Name})", a);
                        return;
                    }

                    var target = propReference.target;

                    var referenceTypeAttribute = f.GetCustomAttributes().OfType<ReferenceTypeAttribute>().FirstOrDefault();
                    if (referenceTypeAttribute != null && !referenceTypeAttribute.type.IsGenericType) {
                        types.Add(referenceTypeAttribute.type);
                    } else {
                        types.Add(typeof(object));
                    }

                    var propertyInfo = ReflectionUtils.GetPublicPropertyInfo(target.GetType(), propReference.fieldName);
                    types.Add(propertyInfo.PropertyType);
                });
            });

            return types;
        }

        private void ScanScriptableObject(string path) {
            
        }

        private void GenerateClass(List<Type> types) {

            var classText = "//This is auto generated class\r\n";

            classText += MakeLine($"public class il_2_cpp_Cache : {typeof(PropertyReferenceCacher).FullName} {{", 0);

            classText += MakeLine("public void Cache() {", 1);
            
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
    }
}