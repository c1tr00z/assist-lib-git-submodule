using c1tr00z.AssistLib.DataModels;

namespace c1tr00z.AssistLib.GameUI {
    public class UIView : DataModelBase, IUIView {

        #region IUIView Implementation

        public void Show(params object[] args) {
            OnShow(args);
            OnDataChanged();
        }

        #endregion

        #region Class Implementation

        protected virtual void OnShow(params object[] args) { }

        #endregion
    }
}