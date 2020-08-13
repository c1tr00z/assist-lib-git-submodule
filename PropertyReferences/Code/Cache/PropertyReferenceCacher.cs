using c1tr00z.AssistLib.Common;

namespace c1tr00z.AssistLib.PropertyReferences {
    public class PropertyReferenceCacher : ICacher {

        public virtual void Cache() { }

        protected void Cache<T>() {
            PropertyValueGetterUtils.AddTypeByGetter(new ValueGetterCache<T>());
        }
    }
}