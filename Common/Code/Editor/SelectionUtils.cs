using System.IO;
using UnityEditor;

namespace c1tr00z.AssistLib.Common {
 
    public static class SelectionUtils {
    
        public static string GetSelectedPath() {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path == "") {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "") {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return path;
        }
    }
}