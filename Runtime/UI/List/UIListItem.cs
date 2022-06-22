using UnityEngine;
using System.Collections;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.GameUI {
    public class UIListItem : MonoBehaviour {

        #region Private Fields

        private UIListItemDBEntry _dbEntry;

        #endregion

        #region Accessors

        public object item { get; private set; }
        
        public UIList list { get; private set; }
        
        public bool isSelected { get; private set; }

        public UIListItemDBEntry dbEntry => this.GetDBEntry(ref _dbEntry);

        #endregion

        #region Class Implementation

        public void Init(UIList list) {
            this.list = list;
        }

        public virtual void UpdateItem(object item) {
            if (this.item != item) {
                this.item = item;
                GetComponents<IUIView>().ToList().ForEach(listItem => listItem.Show(item));
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
            GetComponents<IUIView>().ToList().ForEach(listItem => listItem.Show(item));
        }

        #endregion
    }
}