using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.TypeReferences;
using c1tr00z.AssistLib.Utils;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.GameUI {
    public class UIList : MonoBehaviour {

        #region Nested Classes

        [Serializable]
        private class ObjectEvent : UnityEvent<object> {}

        private class ItemsRequest {
            public List<object> items = new List<object>();
            public object selectedItem;
        }

        #endregion

        #region Events

        public event Action ItemsUpdated;

        #endregion
        
        #region Private Fields

        private List<UIListItem> _listItems = new List<UIListItem>();

        private Dictionary<Type, UIListItem> _listItemPrefabs = new Dictionary<Type, UIListItem>();

        private List<Type> _allAvailableTypes = new List<Type>();

        private Transform _pool;

        private List<UIListItem> _pooled = new List<UIListItem>();

        private Queue<ItemsRequest> _requestQueue = new Queue<ItemsRequest>();

        private bool _isUpdateCoroutineOn = false;

        #endregion

        #region Serialized Fields

        [SerializeField] private List<UIListItemDBEntry> _listItemDBEntries = new List<UIListItemDBEntry>();
        
        [SerializeField] private bool _useSelect;

        [SerializeField] private bool _allowReselectSame;

        [SerializeField] private ObjectEvent _onSelected;

        #endregion

        #region Accessors

        private Transform pool {
            get {
                if (_pool == null) {
                    _pool = new GameObject("Pool").transform;
                    _pool.Reset(transform);
                    _pool.gameObject.SetActive(false);
                }

                return _pool;
            }
        }

        public object selectedValue { get; private set; }

        #endregion

        #region Class Implementation

        public void UpdateList(List<object> items, object selectedItem = default) {
            
            _requestQueue.Enqueue(new ItemsRequest {
                items = items,
                selectedItem = selectedItem,
            });

            if (_isUpdateCoroutineOn) {
                return;
            }

            if (enabled) {
                StartCoroutine(C_UpdateList());
            }
        }

        private IEnumerator C_UpdateList() {
            _isUpdateCoroutineOn = true;

            while (_requestQueue.Count > 0) {

                var request = _requestQueue.Dequeue();
                
                if (_listItemPrefabs.Count == 0) {
                    foreach (var listItemDBEntry in _listItemDBEntries) {
                        var listItemRequest = listItemDBEntry.InstantiatePrefabAsync<UIListItem>();
                        
                        yield return listItemRequest;

                        var listItemInstance = listItemRequest.asset;
                        listItemInstance.Reset(pool);
                        
                        _listItemPrefabs.Add(listItemDBEntry.typeRef.GetRefType(), listItemInstance);
                    }

                    if (_listItemPrefabs.Count > 0) {
                        _allAvailableTypes.AddRange(_listItemPrefabs.Keys);
                    }
                }

                var vacant = _listItems.ToList();
                var order = 0;
                var items = request.items;
                items.ForEach(item => {
                    var listItem = vacant.FirstOrDefault(li => item.GetType() == li.dbEntry.typeRef.GetRefType());

                    if (listItem == null) {
                        listItem = vacant.FirstOrDefault(li => li.dbEntry.typeRef.GetRefType().IsInstanceOfType(item));
                    }

                    if (listItem != null && vacant.Contains(listItem)) {
                        vacant.Remove(listItem);
                    } else {
                        listItem = CreateListItem(item.GetType());
                    }

                    if (listItem == null) {
                        return;
                    }
                    
                    listItem.UpdateItem(item);
                    listItem.transform.SetSiblingIndex(order);
                    order++;
                });

                vacant.ForEach(ReturnToPool);

                ItemsUpdated?.Invoke();
            }
            
            _isUpdateCoroutineOn = false;
        }

        private UIListItem CreateListItem(Type itemType) {
            
            UIListItem listItem = GetFromPool(itemType);

            if (listItem == null) {
                listItem = GetPrefabByType(itemType)?.Clone();
            }

            if (listItem == null) {
                return null;
            }
            
            listItem.transform.SetParent(transform, false);
            listItem.transform.localScale = Vector3.one;

            var rectTransform = listItem.transform as RectTransform;

            listItem.Init(this);
            _listItems.Add(listItem);
            return listItem;
        }

        private UIListItem GetFromPool(Type type) {
            var workingType = _allAvailableTypes.Contains(type)
                ? type
                : _allAvailableTypes.FirstOrDefault(t => t.IsAssignableFrom(type));
            
            if (workingType == null) {
                return null;
            }

            var candidate = _pooled.FirstOrDefault(li => li.dbEntry.typeRef.GetRefType() == workingType);

            if (candidate != null) {
                _pooled.Remove(candidate);
            }

            return candidate;
        }

        private UIListItem GetPrefabByType(Type type) {
            var workingType = _allAvailableTypes.Contains(type)
                ? type
                : _allAvailableTypes.FirstOrDefault(t => t.IsAssignableFrom(type));

            if (workingType == null) {
                return null;
            }

            return _listItemPrefabs[workingType];
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
                return;
            }
            
            selectedValue = item;
            _listItems.ForEach(li => li.SetSelected(li.item == selectedValue));
            _onSelected?.Invoke(selectedValue);
        }

        private void ReturnToPool(UIListItem listItem) {
            listItem.transform.Reset(pool);
            _pooled.Add(listItem);
            if (_listItems.Contains(listItem)) {
                _listItems.Remove(listItem);
            }
        }

        #endregion
    }
}