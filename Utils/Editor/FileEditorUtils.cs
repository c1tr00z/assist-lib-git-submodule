using System.IO;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEditor;
using UnityEngine;

namespace AssistLib.Utils.Editor {
    public static class FileEditorUtils {
        
        public static void SaveTextToFile(TextAsset textAsset, string text) {
            var path = AssetDatabase.GetAssetPath(textAsset);
            SaveTextToFile(path, text);
        }

        public static TextAsset SaveTextToFile(DBEntry dbEntry, string key, string text) {
            var dbEntryPath = AssetDatabase.GetAssetPath(dbEntry).Replace(".asset", "");
            var dataPathSplitted = Application.dataPath.Split(Path.DirectorySeparatorChar);
            dataPathSplitted = dataPathSplitted.SubArray(0, dataPathSplitted.Length - 1).ToArray();
            var assetDirectoryPath = Path.Combine(Path.Combine(dataPathSplitted), dbEntryPath);

            var assetPath = $"{assetDirectoryPath}@{key}.txt";
            
#if UNITY_EDITOR_OSX
            if (!assetPath.StartsWith("/")) {
                assetPath = $"/{assetPath}";
            }
#endif

            SaveTextToFile(assetPath, text);

            return dbEntry.Load<TextAsset>(key);
        }

        public static void SaveTextToFile(string pathToFile, string text) {
            var file = new FileInfo(pathToFile);

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