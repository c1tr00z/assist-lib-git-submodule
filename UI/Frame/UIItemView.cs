using System.Linq;

namespace c1tr00z.AssistLib.UI {
    public abstract class UIItemView<T> : UIView {

        public override bool isDataModelEnabled => item != null;

        public virtual T item { get; protected set; }

        protected override void OnShow(params object[] args) {
            base.OnShow(args);
            item = args.OfType<T>().FirstOrDefault();
        }
    }
}