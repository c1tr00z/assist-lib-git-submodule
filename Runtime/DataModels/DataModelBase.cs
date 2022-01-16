using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {
    public class DataModelBase : MonoBehaviour, IDataModelBase {

        #region Private Field

        private List<IValueReceiver> _valueReceivers = new List<IValueReceiver>();

        #endregion

        #region Accessors

        public virtual bool isDataModelEnabled => true;

        #endregion

        #region Unity Events

        protected virtual void Start() {
            OnDataChanged();
        }

        #endregion

        #region Class Implementation

        public void AddReceiver(IValueReceiver receiver) {
            if (_valueReceivers.Contains(receiver)) {
                return;
            }
            _valueReceivers.Add(receiver);
        }

        public void RemoveReceiver(IValueReceiver receiver) {
            if (!_valueReceivers.Contains(receiver)) {
                return;
            }

            _valueReceivers.Remove(receiver);
        }

        public void OnDataChanged() {
            if (_valueReceivers.Count == 0) {
                return;
            }
            _valueReceivers.Where(r => r.isRecieverEnabled).ToList().ForEach(r => r.UpdateReceiver());
        }

        #endregion
    }
}