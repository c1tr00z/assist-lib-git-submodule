using System;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.Sprites {
    [Serializable]
    public class SpriteAnimatorState {
        public string name;
        public List<Sprite> sprites = new List<Sprite>();
        public List<SpriteAnimatorStateEvent> events = new List<SpriteAnimatorStateEvent>();
        public bool loop;
        public string nextStateName;

        private int _currentFrameIndex;

        public void Activate() {
            _currentFrameIndex = -1;
        }

        public SpriteAnimatorFrameInfo NextFrame() {
            _currentFrameIndex++;
            return CurrentFrame();
        }

        public SpriteAnimatorFrameInfo CurrentFrame() {
            
            if (sprites.Count == 0) {
                throw new UnityException("State is empty");
            }
            
            if (_currentFrameIndex < 0) {
                _currentFrameIndex = 0;
            }
            
            if (_currentFrameIndex >= sprites.Count) {
                _currentFrameIndex = loop ? 0 : sprites.Count - 1;
            }

            var frameInfo = new SpriteAnimatorFrameInfo {
                sprite = sprites[_currentFrameIndex],
                nextStateName = _currentFrameIndex == sprites.Count - 1 ? nextStateName : null,
                events = events.Where(e => e.frame == _currentFrameIndex).ToList(),
            };
            
            return frameInfo;
        }
    }
}