using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.AppModules {
    [Obsolete("Logic moved to Modules submodule")]
    public class App : BehaviourSingleton<App> {

        #region Events

        public static event Action modulesLoaded;

        #endregion

        #region Private Fields

        private Dictionary<ModuleDBEntry, GameObject> _modules = new Dictionary<ModuleDBEntry, GameObject>();

        private Transform _appModulesContainer;
        private Transform _gameModulesContainer;

        #endregion

        #region Unity Events

        IEnumerator Start() {

            DontDestroyOnLoad(gameObject);

            yield return StartCoroutine(C_Modules());

            modulesLoaded.SafeInvoke();
        }

        #endregion

        #region Class Implementation

        IEnumerator C_Modules() {
            var appModules = DB.GetAll<ModuleDBEntry>().Where(m => m.enabled).SelectNotNull().ToList();
            appModules.Sort(new Comparison<ModuleDBEntry>((m1, m2) => { return m1.priority.CompareTo(m2.priority); }));
            _modules = new Dictionary<ModuleDBEntry, GameObject>();
            _appModulesContainer = new GameObject("AppModules").transform;
            _appModulesContainer.Reset(transform);
            foreach (var module in appModules) {
                var moduleGO = module.LoadPrefab<GameObject>().Clone();
                moduleGO.name = module.name;
                moduleGO.transform.parent = _appModulesContainer;
                _modules.Add(module, moduleGO);
                var moduleComponent = moduleGO.GetComponent<ModuleComponent>();
                if (moduleComponent != null) {
                    while (!moduleComponent.inited) {
                        moduleComponent.inited = true;
                        yield return null;
                    }
                }
            }
        }

        public T Get<T>() {

            if (_modules != null) {
                foreach (var kvp in _modules) {
                    var module = kvp.Value.GetComponentInChildren<T>();
                    if (module != null) {
                        return module;
                    }
                }
            }

            return default(T);
        }

        #endregion
    }
}