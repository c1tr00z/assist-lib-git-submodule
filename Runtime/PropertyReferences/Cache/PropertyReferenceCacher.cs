using c1tr00z.AssistLib.Common;
using Cysharp.Threading.Tasks;

namespace c1tr00z.AssistLib.PropertyReferences {
    public abstract class PropertyReferenceCacher : ICacher {

        #region ICacher Implementation

        public virtual UniTask Cache() {
            return UniTask.CompletedTask;
        }

        #endregion

        #region Class Implementation

        protected void Cache<T>() {
            PropertyValueGetterUtils.AddTypeByGetter(new ValueGetterCache<T>());
        }

        #endregion
    }
}