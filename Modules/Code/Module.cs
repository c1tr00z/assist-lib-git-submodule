using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Common.Coroutines;
using c1tr00z.AssistLib.Utils;
using c1tr00z.MagicGuns.Effects;
using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class Module : MonoBehaviour, IModule {

        #region Private Fields

        private Dictionary<Type, EffectBase> _effectsByParameters = new Dictionary<Type, EffectBase>();

        #endregion

        #region Class Implementation

        public virtual void InitializeModule(CoroutineRequest request) {

            ReflectionUtils.GetSubclassesOf<EffectBase>(false).ToUniqueDictionary(
                t => t.BaseType.GenericTypeArguments.FirstOrDefault(), 
                t => (EffectBase)Activator.CreateInstance(t));
            request.Finish();
        }

        #endregion
    }
}