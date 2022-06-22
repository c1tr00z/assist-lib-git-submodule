using System;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.TypeReferences {
    public static class TypeReferenceUtils {

        #region Class Implementation

        public static Type GetRefType(this TypeReference typeReference) {
            return ReflectionUtils.GetTypeByName(typeReference.typeFullName);
        }

        #endregion
    }
}