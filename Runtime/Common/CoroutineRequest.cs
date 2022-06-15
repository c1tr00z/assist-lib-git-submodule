namespace c1tr00z.AssistLib.Common {
    public class CoroutineRequest : RequestBase {
    
        #region Class Implementation
    
        public static CoroutineRequest MakeFinishedRequest() {
            var request = new CoroutineRequest();
    
            request.Finish();
            
            return request;
        }
    
        #endregion
    }
}