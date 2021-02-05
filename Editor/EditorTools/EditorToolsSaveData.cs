using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.Json;

namespace c1tr00z.AssistLib.EditorTools {
    public class EditorToolsSaveData : IJsonSerializable, IJsonDeserializable {

        #region Nested Classes

        [Serializable]
        public class EditorToolData : IJsonSerializable, IJsonDeserializable {
            [JsonSerializableField]
            public string typeName;
            [JsonSerializableField]
            public Dictionary<string, object> data = new Dictionary<string, object>();
        }

        #endregion

        #region JSON Fields

        [JsonSerializableField]
        public List<EditorToolData> toolsData = new List<EditorToolData>();

        #endregion
        
    }
}