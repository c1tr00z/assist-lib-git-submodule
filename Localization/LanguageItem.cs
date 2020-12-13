using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;

namespace c1tr00z.AssistLib.Localization {
    
    public class LanguageItem : DBEntry {

        #region Private Fields

        private Dictionary<string, string> _translations;

        #endregion

        #region Accessors

        public Dictionary<string, string> translations {
            get {
                if (_translations == null || _translations.Count == 0) {
                    _translations = new Dictionary<string,string>();
                    var langHash = JSONUtuls.Deserialize(this.LoadText());
                    langHash.ToList().ForEach(kvp => _translations.AddOrSet(kvp.Key, kvp.Value.ToString()));
                }
                return _translations;
            }
        }

        #endregion
    }
}
