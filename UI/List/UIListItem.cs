using UnityEngine;
using System.Collections;
using System.Linq;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.GameUI {
    public class UIListItem : MonoBehaviour {

        #region Accessors

        public object item { get; private set; }
        
        public UIList list { get; private set; }
        
        public bool isSelected { get; private set; }

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