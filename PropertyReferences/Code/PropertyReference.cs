using System.Reflection;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
    /**
     * <summary>Basic type for property references subsystem.
     * Allows to set links to properties in unity without hardcode it.
     * Requires generate cacher class in Property Reference tool (Open tools windows with Assist > Tools, Open Property
     * Reference tool and click "Generate class" button. This system uses reflections.</summary>
     */
    [System.Serializable]
    public class PropertyReference {

        #region Public Fields

        public Object target;

        public string fieldName;

        public PropertyValueGetter getter;

        #endregion
    }
}