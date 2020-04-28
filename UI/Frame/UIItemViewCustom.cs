namespace c1tr00z.AssistLib.UI {
    public abstract class UIItemViewCustom<T> : UIItemView<T> {
        public override T item {
            get => GetItem();
            protected set { }
        }

        protected abstract T GetItem();
    }
}