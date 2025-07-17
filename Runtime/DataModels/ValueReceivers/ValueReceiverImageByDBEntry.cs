using System;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.ResourcesManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.DataModels {
    public class ValueReceiverImageByDBEntry : ValueReceiverBase {

        #region Private Fields

        private DBEntry _currentDBEntry;

        private string _hashCode;

        #endregion
        
        #region Public Fields

        public Image image;

        [ReferenceType(typeof(DBEntry))]
        public PropertyReference _dbEntryRef;

        public string key;

        #endregion

        #region ValueReceiverBase Implementation

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _dbEntryRef;
        }

        public override void UpdateReceiver() {
            var newDBEntry = _dbEntryRef.Get<DBEntry>();

            if (newDBEntry == _currentDBEntry && image.sprite != null &&
                image.sprite.name.StartsWith(newDBEntry.name)) {
                
                return;
            }

            _hashCode = Guid.NewGuid().ToString();
            _currentDBEntry = newDBEntry;
            if (!enabled || !gameObject.activeSelf || !gameObject.activeInHierarchy) {
                return;
            }
            
            Load();
        }

        #endregion

        #region Class Implementation

        private async UniTask Load() {
            if (_currentDBEntry.TryDownloadedAsset(key, out Sprite sprite)) {
                image.sprite = sprite;
            }

            await LoadSprite(_currentDBEntry);
        }

        private async UniTask LoadSprite(DBEntry dbEntry) {
            var newSprite = await dbEntry.LoadSpriteAsync(key);
            
            if (dbEntry != _currentDBEntry) {
                return; 
            }
            
            image.sprite = newSprite;
        }

        #endregion
    }
}