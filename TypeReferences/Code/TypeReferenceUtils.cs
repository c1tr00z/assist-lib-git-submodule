using System;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.TypeReferences {
    public static class TypeReferenceUtils {

        #region Class Implementation

        public static Type GetTypeFromReference(this TypeReference typeReference) {
            return ReflectionUtils.GetTypeByName(typeReference.typeFullName);
        }

        #endregion
    }
}