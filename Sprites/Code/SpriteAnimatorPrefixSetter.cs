using UnityEngine;

namespace c1tr00z.AssistLib.Sprites {
    public class SpriteAnimatorPrefixSetter : MonoBehaviour {

        #region Private Fields

        private string _currentPrefix;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private string _defaultPrefix;
        [SerializeField] private SpriteAnimator _spriteAnimator;

        #endregion

        #region Accessors

        public string currentPrefix {
            get {
                if (string.IsNullOrEmpty(_currentPrefix)) {
                    _currentPrefix = _defaultPrefix;
                }

                return _currentPrefix;
            }
            protected set => _currentPrefix = value;
        }
        
        public string currentStateName { get; private set; }

        public SpriteAnimator spriteAnimator => _spriteAnimator;

        public string fullStateName => $"{currentPrefix}{currentStateName}";

        #endregion

        #region Class Implementation

        public void PlayState(string stateName) {
            currentStateName = stateName;
            _spriteAnimator.PlayState(fullStateName);
        }

        #endregion

    }
}