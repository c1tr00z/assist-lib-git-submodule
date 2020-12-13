using UnityEngine;
using System.Collections;

namespace c1tr00z.AssistLib.ResourcesManagement {

    public class DBEntryResource : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private DBEntry _dbEntry;

        [SerializeField] private string _key;

        #endregion

        #region Accessors

        public DBEntry parent => _dbEntry;

        public string key => _key;

        #endregion

        #region Class Implementation

        public void SetParent(DBEntry newParent, string newKey) {
            _dbEntry = newParent;
            _key = newKey;
        }

        #endregion
    }
}
