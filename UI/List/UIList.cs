using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine.Events;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.GameUI {
    public class UIList : MonoBehaviour {

        #region Nested Classes

        [Serializable]
        private class ObjectEvent : UnityEvent<object> {}

        #endregion
        
        #region Private Fields

        private List<UIListItem> _listItems;

        #endregion

        #region Serialized Fields

        [SerializeField] private UIListItemDBEntry listItemDBEntry;
        
        [SerializeField] private bool _useSelect;

        [SerializeField] private bool _allowReselectSame;

        [SerializeField] private ObjectEvent _onSelected;

        #endregion

        #region Accessors

        public object selectedValue { get; private set; }

        #endregion

        #region Class Implementation

        public void UpdateList<T>(List<T> items, T selectedItem = default(T)) {
            if (_listItems != null) {
                foreach (var listItem in _listItems) {
                    Destroy(listItem.gameObject);
                }
            }

            _listItems = new List<UIListItem>();

            foreach (var item in items) {
                CreateListItem(item);
            }

            if (_useSelect && (selectedItem != null && !selectedItem.Equals(default(T))) && _listItems.Count > 0 &&!_listItems.Any(li => li.isSelected)) {
                Select(_listItems.FirstOrDefault());
            }
        }

        private UIListItem CreateListItem(object item) {
            var listItem = listItemDBEntry.LoadPrefab<UIListItem>().Clone();
            listItem.transform.SetParent(transform, false);
            listItem.transform.localScale = Vector3.one;

            var rectTransform = listItem.transform as RectTransform;

            listItem.Init(this);
            listItem.UpdateItem(item);
            _listItems.Add(listItem);
            return listItem;
        }

        public void Select(UIListItem item) {
            if ((!_allowReselectSame && item.isSelected) || !_listItems.Contains(item)) {
                return;
            }
            
            _listItems.ForEach(li => li.SetSelected(li == item));

            selectedValue = item.item;
            
            _onSelected?.Invoke(selectedValue);
        }

        public void SelectValue(object item) {
            if (item == null) {
                selectedValue = null;
                _listItems.ForEach(li => li.SetSelected(li.item == null));
                _onSelected?.Invoke(null);
            }
            
            selectedValue = item;
            _listItems.ForEach(li => li.SetSelected(li.item == selectedValue));
            _onSelected?.Invoke(selectedValue);
        }

        #endregion
    }
}