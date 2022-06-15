using System;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourcesManagement.Editor;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules.Editor {
    public static class ModulesEditorUtils {

        #region Class Implementation

        public static void CreateModule<T>(string moduleName, Action<T> onCreate) where T : Component {
            PathUtils.CreatePath("AssistLib", "Resources", "Modules");
            var moduleDBEntry = ScriptableObjectsEditorUtils.Create<ModuleDBEntry>(PathUtils.Combine("Assets", "AssistLib", "Resources", "Modules"), moduleName);
            PrefabEditorUtils.CreatePrefab(moduleDBEntry, onCreate);
            DBEntryEditorActions.CollectItems();
        }

        #endregion
    }
}