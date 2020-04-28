using c1tr00z.AssistLib.DataModels;

namespace c1tr00z.AssistLib.UI {
    public abstract class UIListItemViewBase<T> : UIItemView<T> {

        private UIListItem _listItem;

        protected UIListItem listItem => this.GetCachedComponent(ref _listItem);

        public void UpdateItem(object item) {
            this.item = (T)item;
            if (this.item != null) {
                UpdateView();
            }
        }

        protected void UpdateView() {
            OnDataChanged();
        }
    }
}