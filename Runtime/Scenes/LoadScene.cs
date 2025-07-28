using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace c1tr00z.AssistLib.SceneManagement {
    public class LoadScene : MonoBehaviour {

        #region Serialized Fields

        [DBEntryType(typeof(SceneItem))]
        [SerializeField] private DBEntryReference _sceneItemRef;

        [SerializeField] private bool _loadOnStart;

        [SerializeField] private bool _force;

        #endregion

        #region Unity Events

        private void Start() {
            if (_loadOnStart) {
                Load();
            }
        }

        #endregion

        #region Class Implementation

        public void Load() {
            _sceneItemRef.GetDBEntry<SceneItem>().Load(_force);
        }
        
        public void LoadAsync() {
            _sceneItemRef.GetDBEntry<SceneItem>().LoadAsync(_force).Forget();
        }
        
        public void LoadAdditiveAsync() {
            _sceneItemRef.GetDBEntry<SceneItem>().LoadAdditiveAsync(_force).Forget();
        }

        #endregion

    }
}