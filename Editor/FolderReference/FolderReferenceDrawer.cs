using System.IO;
using c1tr00z.AssistLib.FolderReference;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace AssistLib.Editor.FolderReference {
    [CustomPropertyDrawer(typeof(FolderReferenceAttribute))]
    public class FolderReferenceDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var textFieldRect = new Rect(position.x, position.y, position.width - (position.height + 4) * 2, position.height);

            var newPath = EditorGUI.TextField(textFieldRect, property.stringValue);

            if (GUI.changed) {
                property.stringValue = newPath;
            }

            var browseButtonRect = new Rect(textFieldRect.x + textFieldRect.width + 2, position.y, textFieldRect.height,
                textFieldRect.height);

            if (GUI.Button(browseButtonRect, "...")) {
                var selectedFolder = property.stringValue.IsNullOrEmpty()
                    ? Application.dataPath
                    : Path.Combine(Application.dataPath, property.stringValue.Substring("Assets/".Length));
                var selectedPath = EditorUtility.OpenFolderPanel("Choose folder", selectedFolder, "");
                if (selectedPath.StartsWith(Application.dataPath)) {
                    selectedPath = Path.Combine("Assets", selectedPath.Substring(Application.dataPath.Length + 1));
                    property.stringValue = selectedPath;
                } else {
                    Debug.LogWarning($"Wrong path picked, abort ({selectedPath})");
                }
            }
            
            var pingButtonRect = new Rect(browseButtonRect.x + browseButtonRect.width + 2, browseButtonRect.y, browseButtonRect.height,
                browseButtonRect.height);
            
            if (GUI.Button(pingButtonRect, "")) {
                AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath(property.stringValue, typeof(Object)));
            }
            
            EditorGUI.EndProperty();
        }
    }
}