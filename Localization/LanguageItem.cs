using System.Collections.Generic;
using c1tr00z.AssistLib.ResourcesManagement;

public class LanguageItem : DBEntry {

    private Dictionary<string, string> _translations;

    public Dictionary<string, string> translations {
        get {
            if (_translations == null || _translations.Count == 0) {
                _translations = new Dictionary<string,string>();
                var langHash = JSONUtuls.Deserialize(this.LoadText());
                langHash.ForEach(kvp => _translations.AddOrSet(kvp.Key, kvp.Value.ToString()));
            }
            return _translations;
        }
    }
}
