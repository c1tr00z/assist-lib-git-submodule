using System.Collections.Generic;

namespace c1tr00z.AssistLib.DataModels {
    public interface IValueReceiver {
        
        bool isReceiverEnabled { get; }
        
        void UpdateReceiver();

        IEnumerable<IDataModelBase> GetModels();
    }
}
