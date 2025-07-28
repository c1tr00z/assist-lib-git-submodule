using c1tr00z.AssistLib.DataProviders;

namespace c1tr00z.AssistLib.GameUI {
    public class UIView : DataProviderBase, IUIView {

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