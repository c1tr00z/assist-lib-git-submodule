using System;
using System.Collections;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public class MainCacher : MonoBehaviour {

        #region Class Implementation

        public async UniTask Cache() {
            var allCachers = ReflectionUtils.GetTypesByInterface(typeof(ICacher), false)
                .Select(t => Activator.CreateInstance(t) as ICacher).ToList();
            foreach (var cacher in allCachers) {
                Debug.Log($"Caching... {cacher.GetType()}");
                await cacher.Cache();
            }
        }

        #endregion
    }
}