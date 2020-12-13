using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.AssistLib.GameUI {
    public class UILayerSimple : UILayerBase {

        #region Private Fields

        private List<UIFrame> _currentFrames = new List<UIFrame>();

        #endregion

        #region Accessors

        public override List<UIFrame> currentFrames => _currentFrames;

        #endregion

        #region UILayerBase Implementation

        public override void Close(UIFrameDBEntry frameDBEntry) {
            var frames = currentFrames.Where(f => f.GetComponent<DBEntryResource>().parent == frameDBEntry).ToList();
            _currentFrames.RemoveAll(f => frames.Contains(f));
            frames.ForEach(f => Destroy(f.gameObject));
        }

        public override void Show(UIFrameDBEntry frameDBEntry, params object[] args) {
            _currentFrames.Add(ShowFrame(frameDBEntry, args));
        }

        #endregion
    }
}
