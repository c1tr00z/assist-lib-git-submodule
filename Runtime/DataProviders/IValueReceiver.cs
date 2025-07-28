using System.Collections.Generic;

namespace c1tr00z.AssistLib.DataProviders {
    public interface IValueReceiver {
        
        bool isReceiverEnabled { get; }
        
        void UpdateReceiver();

        IEnumerable<IDataProviderBase> GetModels();
    }
}
