using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.AssistLib.Settings {
    public class AppSettings : DBEntry {

        #region Serialized Fields

        [SerializeField] private SceneItem _startScene;
        
        [SerializeField] private UIFrameDBEntry _startFrame;

        #endregion

        #region Accessors

        public static AppSettings instance => DB.Get<AppSettings>("AppSettings");

        public SceneItem startScene => _startScene;

        public UIFrameDBEntry startFrame => _startFrame;

        #endregion
    }
}
