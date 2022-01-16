using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.AssistLib.Sprites {
    public class SpriteAnimatorFrameInfo {
        public Sprite sprite;
        public List<SpriteAnimatorStateEvent> events = new List<SpriteAnimatorStateEvent>();
        public string nextStateName;
    }
}