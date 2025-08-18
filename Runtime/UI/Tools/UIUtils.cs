using System;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.Utils;

namespace AssistLib.Runtime.UI.Tools {
    public static class UIUtils {
        #region Private Fields

        private static IUIFrameShowProcessor[] _framePostProcessors = null;
        
        private static IUIListItemsShowPostprocessor[] _listItemsPostProcessors = null;

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

        public static void PreUpdateListItem(UIListItem uiListItem) {
            if (_listItemsPostProcessors == null) {
                _listItemsPostProcessors = ReflectionUtils.GetTypesByInterface<IUIListItemsShowPostprocessor>()
                    .SelectNotNull(t => (IUIListItemsShowPostprocessor)Activator.CreateInstance(t)).ToArray();
            }
            
            foreach (var preProcessor in _listItemsPostProcessors) {
                preProcessor.PreUpdateItem(uiListItem);
            }
        }

        #endregion
    }
}