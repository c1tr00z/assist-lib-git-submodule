using System.IO;

namespace c1tr00z.AssistLib.Utils {
    public static class FileUtils {

        #region Class Implementation

        public static void SaveTextToFile(string fileName, string path, string text) {
            var fullFileName = PathUtils.Combine(path, fileName);
            SaveTextToFile(fullFileName, text);
        }

        public static void SaveTextToFile(string fullFileName, string text) {
            var file = new FileInfo(fullFileName);
            if (!file.Exists) {
                file.Create().Close();
            }

            using (StreamWriter sw = new StreamWriter(file.ToString())) {
                sw.Write(text);
                sw.Close();
            }
        }

        #endregion
    }
}
