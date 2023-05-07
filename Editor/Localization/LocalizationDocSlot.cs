using System.Collections.Generic;
using c1tr00z.AssistLib.Json;

namespace AssistLib.Editor.Localization {
    public abstract class LocalizationDocSlot : IJsonSerializable, IJsonDeserializable {

        #region Class Implementation

        public abstract void DrawSlotGUI();

        public abstract Dictionary<string, Dictionary<string, string>> Import();

        #endregion

    }
}