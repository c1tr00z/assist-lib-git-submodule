using System;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.Utils;

namespace AssistLib.Runtime.UI.Tools {
    public static class UIUtils {
        #region Private Fields

        private static IUIFrameShowProcessor[] _framePostProcessors = null;

        #endregion

        #region Class Implementation
        
        public static void PreShow(UIFrame uiFrame) {
            if (_framePostProcessors == null) {
                _framePostProcessors = ReflectionUtils.GetTypesByInterface<IUIFrameShowProcessor>()
                    .SelectNotNull(t => (IUIFrameShowProcessor)Activator.CreateInstance(t)).ToArray();
            }

            foreach (var showProcessor in _framePostProcessors) {
                showProcessor.PreShow(uiFrame);
            }
        }

        #endregion
    }
}