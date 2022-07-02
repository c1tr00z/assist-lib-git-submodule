using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.GameUI {
    public class UIListItem : DataModelBase {

        #region Private Fields

        private UIListItemDBEntry _dbEntry;

        private List<UIView> _views = new List<UIView>();

        #endregion

        #region Accessors

        public object item { get; private set; }
        
        public UIList list { get; private set; }
        
        public bool isSelected { get; private set; }

        public UIListItemDBEntry dbEntry => this.GetDBEntry(ref _dbEntry);

        private List<UIView> views => this.GetCachedComponents(ref _views);

        #endregion

        #region Class Implementation

        public void Init(UIList list) {
            this.list = list;
        }

        public virtual void UpdateItem(object item) {
            if (this.item != item) {
                this.item = item;
                views.ForEach(listItem => listItem.Show(item));
            }
        }

        public void Select() {
            list.Select(this);
        }

        public void SetSelected(bool selected) {
            if (selected == isSelected) {
                return;
            }
            isSelected = selected;
            views.ForEach(listItem => listItem.Show(item));
            OnDataChanged();
        }

        #endregion
    }
}