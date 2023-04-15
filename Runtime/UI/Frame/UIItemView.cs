using System.Linq;

namespace c1tr00z.AssistLib.GameUI {
    public abstract class UIItemView<T> : UIView {

        #region Accessors

        public override bool isDataModelEnabled => item != null;
        
        public virtual T item { get; protected set; }

        #endregion

        #region UIView Implementation

        protected override void OnShow(params object[] args) {
            item = args.OfType<T>().FirstOrDefault();
            base.OnShow(args);
        }

        #endregion
    }
}