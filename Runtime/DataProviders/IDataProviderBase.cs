namespace c1tr00z.AssistLib.DataProviders {
    public interface IDataProviderBase {
        bool isDataModelEnabled { get; }
        void OnDataChanged();
        void AddReceiver(IValueReceiver receiver);
        void RemoveReceiver(IValueReceiver receiver);
    }
}