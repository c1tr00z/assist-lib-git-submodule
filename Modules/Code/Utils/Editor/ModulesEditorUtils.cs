using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.DataBase.Editor;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules.Editor {
    public static class ModulesEditorUtils {

        public static void CreateModule<T>(string moduleName, System.Action<T> onCreate) where T : Component {
            PathUtils.CreatePath("AssistLib", "Resources", "Modules");
            var moduleDBEntry = ScriptableObjectsEditorUtils.Create<ModuleDBEntry>(PathUtils.Combine("Assets", "AssistLib", "Resources", "Modules"), moduleName);
            PrefabEditorUtils.CreatePrefab(moduleDBEntry, onCreate);
            DBEntryEditorUtils.CollectItems();
        }
    }
}