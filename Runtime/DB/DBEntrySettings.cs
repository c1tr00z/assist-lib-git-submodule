using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Addressables;
using c1tr00z.AssistLib.TypeReferences;
using UnityEngine;

namespace c1tr00z.AssistLib.ResourcesManagement {
    public class DBEntrySettings : ScriptableObject {

        #region Nested Classes

        [Serializable]
        public class DBEntryEntry {
            [BaseType(typeof(DBEntry))]
            public TypeReference dbEntryType;

            public AddressableGroupRef groupRef;
        }

        #endregion

        #region Public Fields

        public List<DBEntryEntry> settings = new List<DBEntryEntry>();

        #endregion

    }
}