﻿using c1tr00z.AssistLib.DataBase.Editor;
using UnityEngine;

namespace c1tr00z.AssistLib.Modules.Editor {
    public static class ModulesEditorUtils {

        public static void CreateModule<T>(string moduleName, System.Action<T> onCreate) where T : Component {
            PathUtils.CreatePath("AssistLib", "Resources", "Modules");
            var moduleDBEntry = AssetsUtils.CreateScriptableObject<ModuleDBEntry>(PathUtils.Combine("Assets", "AssistLib", "Resources", "Modules"), moduleName);
            AssetsUtils.CreatePrefab(moduleDBEntry, onCreate);
            ItemsEditor.CollectItems();
        }
    }
}