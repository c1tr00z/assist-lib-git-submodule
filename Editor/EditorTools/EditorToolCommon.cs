using System.IO;
using System.Linq;
using AssistLib.Utils.Editor;
using UnityEngine;

namespace c1tr00z.AssistLib.EditorTools {
    [EditorToolName("Common editor tool")]
    public class EditorToolCommon : EditorTool
    {

        #region EditorTool Implementation

        public override void DrawInterface() {
            if (Button("Generate layers class")) {
                GenerateLayersEnum();
            }
        }

        #endregion

        #region Class Implementation

        private void GenerateLayersEnum() {
            var codeText = "";

            codeText += "public enum GameLayers {\r\n";

            for (int i = 0; i < 32; i++) {
                var layerName = ReformatString(LayerMask.LayerToName(i));
                if (!string.IsNullOrEmpty(layerName)) {
                    codeText += $"\t{layerName} = {i},\r\n";
                }
            }
            
            codeText += "}\r\n";
            
            SaveToFile(codeText);
        }

        private void SaveToFile(string codeText) {
            var pathToFolder = Path.Combine(Application.dataPath, "Code", "Runtime", "Common");
            if (!Directory.Exists(pathToFolder)) {
                Directory.CreateDirectory(pathToFolder);
            }
            var pathToFile = Path.Combine(pathToFolder, "GameLayers.cs");
            FileEditorUtils.SaveTextToFile(pathToFile, codeText);
        }

        private string ReformatString(string original) {
            var names = System.Text.RegularExpressions.Regex.Replace(original, "([A-Z])",
                " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim().Split(' ');

            names = names.ToList().Select(n => n.Trim().ToUpper()).ToArray();

            return string.Join("_", names);
        }

        #endregion
    }
}