using UnityEngine;
using System.Collections;

namespace c1tr00z.AssistLib.ResourcesManagement {

    /**
     * <summary>Every time Assist > Collect items called this script attached to any X@Prefab in system, where X is
     * name of DBEntry</summary>
     */
    public class DBEntryResource : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private DBEntryReference _dbEntryRef;

        [SerializeField] private string _key;

        #endregion

        #region Accessors

        public DBEntry parent => _dbEntryRef.GetDBEntry<DBEntry>();

        public string key => _key;

        #endregion

        #region Class Implementation

        public void SetParent(DBEntry newParent, string newKey) {
            _dbEntryRef.dbEntryName = newParent != null ? newParent.name : null;
            _key = newKey;
        }

        #endregion
    }
}
