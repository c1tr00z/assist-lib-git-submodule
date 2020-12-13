using System;

namespace c1tr00z.AssistLib.PropertyReferences {
    public abstract class ValueGetterCache {

        #region Accessors

        public abstract Type genericType { get; } 

        #endregion

        #region Abstract Methods

        public abstract PropertyValueGetter MakeValueGetter();

        #endregion
    }
    
    public class ValueGetterCache<T> : ValueGetterCache {

        #region ValueGetterCache Implementation

        public override Type genericType => typeof(T);

        public override PropertyValueGetter MakeValueGetter() {
            return new PropertyValueGetter<T>();
        }
        
        #endregion

    }
}