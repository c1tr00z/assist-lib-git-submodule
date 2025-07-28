using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.PropertyReferences;

namespace c1tr00z.AssistLib.DataProviders {
    public abstract class DataTranslator : DataProviderBase, IValueReceiver {

        #region Private Fields

        private List<IDataProviderBase> _models = new List<IDataProviderBase>();

        #endregion

        #region Unity Events

        protected virtual void Awake() {
            GetModels().ToList().ForEach(m => m.AddReceiver(this));
        }

        #endregion
        
        #region IValueReceiver Implementation

        public virtual bool isReceiverEnabled {
            get { return GetModels().All(m => m.isDataModelEnabled); }
        }
        
        public abstract void UpdateReceiver();

        public IEnumerable<IDataProviderBase> GetModels() {
            if (_models == null || _models.Count == 0) {
                _models = new List<IDataProviderBase>();
                var references = GetReferences();
                while (references.MoveNext()) {
                    var reference = references.Current;
                    var model = reference.target as IDataProviderBase;
                    if (model == null) {
                        continue;
                    }
                    if (!_models.Contains(model)) {
                        _models.Add(model);
                    }
                }
            }
                
            return _models;
        }

        #endregion

        #region Abstract Methods

        public abstract IEnumerator<PropertyReference> GetReferences();

        #endregion
    }
}