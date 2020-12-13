using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.Sprites {
    public class Flipbook : ScriptableObject
    {
        #region Serialized Fields

        [SerializeField]
        private List<Sprite> _sprites = new List<Sprite>();

        [SerializeField] private List<FlipbookAnimationEvent> _events = new List<FlipbookAnimationEvent>();

        [SerializeField] private float _fps;

        #endregion

        #region Accessors

        public float fps => _fps;

        public int length => _sprites.Count;

        #endregion

        #region Overloads

        public Sprite this[int index] {
            get {
                if (index >= _sprites.Count || index < 0) {
                    throw new IndexOutOfRangeException();
                }

                return _sprites[index];
            }
        }

        #endregion

        #region Class Implementation

        public Sprite GetSprite(int index) {
            return this[index];
        }

        public List<FlipbookAnimationEvent> GetEvents(int frame) {
            return _events.Where(e => e.frame == frame).ToList();
        }

        #endregion
    }
}

