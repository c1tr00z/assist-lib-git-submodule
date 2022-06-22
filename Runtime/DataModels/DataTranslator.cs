using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.PropertyReferences;

namespace c1tr00z.AssistLib.DataModels {
    public abstract class DataTranslator : DataModelBase, IValueReceiver {

        #region Private Fields

        private List<IDataModelBase> _models = new List<IDataModelBase>();

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

        public IEnumerable<IDataModelBase> GetModels() {
            if (_models == null || _models.Count == 0) {
                _models = new List<IDataModelBase>();
                var references = GetReferences();
                while (references.MoveNext()) {
                    var reference = references.Current;
                    var model = reference.target as IDataModelBase;
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