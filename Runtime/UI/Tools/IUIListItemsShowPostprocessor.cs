using c1tr00z.AssistLib.GameUI;

namespace AssistLib.Runtime.UI.Tools {
    public interface IUIListItemsShowPostprocessor {
        #region Methods

        public void PreUpdateItem(UIListItem listItem);

        #endregion
    }
}