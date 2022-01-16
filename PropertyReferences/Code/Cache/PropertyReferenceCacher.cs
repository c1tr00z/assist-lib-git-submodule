using c1tr00z.AssistLib.Common;

namespace c1tr00z.AssistLib.PropertyReferences {
    public abstract class PropertyReferenceCacher : ICacher {

        #region ICacher Implementation

        public virtual void Cache() { }

        #endregion

        #region Class Implementation

        protected void Cache<T>() {
            PropertyValueGetterUtils.AddTypeByGetter(new ValueGetterCache<T>());
        }

        #endregion
    }
}