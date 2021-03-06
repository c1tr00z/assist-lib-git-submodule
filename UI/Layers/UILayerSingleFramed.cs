﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    public class UILayerSingleFramed : UILayerBase {

        #region Private Fields

        private UIFrame _currentFrame;

        private UIFrameDBEntry _currentFrameDBEntry;

        #endregion

        #region Accessors

        public override List<UIFrame> currentFrames => new List<UIFrame> { _currentFrame };

        #endregion

        #region Class Implementation

        public override void Close(UIFrameDBEntry frameDBEntry) {
            if (_currentFrameDBEntry == frameDBEntry) {
                Destroy(_currentFrame.gameObject);
                _currentFrameDBEntry = null;
            }
        }

        private void Close(UIFrame frame) {
            if (frame == _currentFrame) {
                Close(_currentFrameDBEntry);
            }
        }

        public override void Show(UIFrameDBEntry frame, params object[] args) {
            if (_currentFrameDBEntry == frame) {
                return;
            }
            var prevFrame = _currentFrame;
            _currentFrameDBEntry = frame;
            _currentFrame = ShowFrame(frame, args);
            if (prevFrame != null) {
                Destroy(prevFrame.gameObject);
            }
        }

        #endregion
    }
}