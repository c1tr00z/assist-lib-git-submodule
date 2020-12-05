using System;
using AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.Sprites {
    public class FlipbookPlayer : MonoBehaviour {

        #region Events

        public event Action<string> animationEvent;
        
        public event Action<Flipbook> animationFinished;

        #endregion
        
        #region Nested Classes

        [Serializable]
        public class AnimationEvent : UnityEvent<string> {
        };
        
        [Serializable]
        public class FlipbookEvent : UnityEvent<Flipbook> {
        };

        #endregion

        #region Private Fields

        private int _frame;

        private float _lastTime;

        #endregion

        #region Serialized Fields

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private AnimationEvent _animationEvent;
        
        [SerializeField]
        private FlipbookEvent _animationFinishedEvent;

        [SerializeField]
        private Flipbook _flipbook;

        #endregion

        #region Accessors

        public Flipbook flipbook {
            get => _flipbook;
            set {
                _flipbook = value;
                RestartAnimation();
            }
        }

        public float requiredSeconds => flipbook.IsAssigned() ? 1f / flipbook.fps : 0;

        #endregion

        #region Unity Classes

        private void FixedUpdate() {
            UpdateSprite();
        }

        #endregion

        #region Class Implementation

        private void UpdateSprite() {
            if (!flipbook.IsAssigned()) {
                return;
            }

            if (flipbook.length == 0) {
                throw new UnityException($"Incorrect flipbook: {requiredSeconds}");
            }

            if (flipbook.fps == 0) {
                throw new UnityException($"Flipbook.fps cannot be less or equal 0: {requiredSeconds}");
            }

            if (Time.time - _lastTime < requiredSeconds) {
                return;
            }

            if (_frame >= flipbook.length) {
                // _frame = 0;
                OnAnimationFinished();
                return;
            }

            _lastTime = Time.time;

            _spriteRenderer.sprite = _flipbook[_frame];

            var events = flipbook.GetEvents(_frame);
            events.ForEach(OnAnimationEvent);

            _frame++;
        }

        public void RestartAnimation() {
            _frame = 0;
        }
        
        private void OnAnimationEvent(FlipbookAnimationEvent flipbookEvent) {
            OnAnimationEvent(flipbookEvent.eventName);
        }

        private void OnAnimationEvent(string eventName) {
            _animationEvent.SafeInvoke(eventName);
            animationEvent?.Invoke(eventName);
        }

        private void OnAnimationFinished() {
            animationFinished?.Invoke(flipbook);
            _animationFinishedEvent.SafeInvoke(flipbook);
        }

        #endregion
    }
}