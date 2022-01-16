namespace c1tr00z.AssistLib.GameUI {
    public abstract class UIItemViewCustom<T> : UIItemView<T> {

        #region Accessors

        public override T item {
            get => GetItem();
            protected set { }
        }

        #endregion

        #region Abstract Methods

        protected abstract T GetItem();

        #endregion
    }
}