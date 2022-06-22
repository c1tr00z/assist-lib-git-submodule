using System;
using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.ResourcesManagement;
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
            StartCoroutine(C_Load(_currentDBEntry));
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_Load(DBEntry dbEntry) {
            var request = dbEntry.LoadSpriteAsync(key);
            
            yield return request;
            
            if (dbEntry != _currentDBEntry) {
                yield break; 
            }

            image.sprite = request.asset;
        }

        #endregion
    }
}