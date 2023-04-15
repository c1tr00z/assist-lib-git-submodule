using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class Submodules : Module {

        #region Private Fields

        private Dictionary<Type, Type> _submodulesTypes = new();

        private Dictionary<Type, SubmoduleBase> _submodules = new();

        #endregion

        #region Unity Events

        private void OnEnable() {
            Modules.ModuleInitialized += OnModuleInitialized;
        }

        private void OnDisable() {
            Modules.ModuleInitialized -= OnModuleInitialized;
        }

        #endregion

        #region Module Implementation

        public override void InitializeModule(CoroutineRequest request) {
            _submodulesTypes = ReflectionUtils.GetSubclassesOf<SubmoduleBase>(false)
                .ToUniqueDictionary(t => t.BaseType.GenericTypeArguments.FirstOrDefault(), t => t);
            base.InitializeModule(request);
        }

        #endregion

        #region Class Implementation

        private void OnModuleInitialized(Module module) {
            var moduleType = module.GetType();
            if (_submodules.ContainsKey(moduleType)) {
                _submodules[moduleType].Init(module);
            }
            if (!_submodulesTypes.ContainsKey(moduleType)) {
                return;
            }

            var submoduleType = _submodulesTypes[moduleType];
            var submodule = new GameObject(submoduleType.Name).AddComponent(submoduleType) as SubmoduleBase;
            if (submodule == null) {
                Debug.LogError($"[SUBMODULES] Suddenly {submoduleType.FullName} is not submodule?");
                return;
            }
            submodule.Init(module);
            submodule.transform.Reset(transform);
            _submodules.Add(moduleType, submodule);
        }

        public T Get<T>() where T : SubmoduleBase {
            return _submodules.Values.OfType<T>().FirstOrDefault();
        }

        #endregion
    }
}