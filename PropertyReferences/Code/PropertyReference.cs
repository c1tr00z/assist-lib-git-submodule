using System.Reflection;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
    [System.Serializable]
    public class PropertyReference {

        #region Public Fields

        public Object target;

        public string fieldName;

        public PropertyValueGetter getter;

        #endregion
    }
}