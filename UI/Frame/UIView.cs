using c1tr00z.AssistLib.DataModels;

namespace c1tr00z.AssistLib.GameUI {
    public class UIView : DataModelBase, IUIView {
        public void Show(params object[] args) {
            OnShow(args);
            OnDataChanged();
        }

        protected virtual void OnShow(params object[] args) { }
    }
}