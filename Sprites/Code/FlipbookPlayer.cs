using System;
using c1tr00z.AssistLib.Utils;
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

        public float requiredSeconds => flipbook.IsNull() ? 0 : 1f / flipbook.fps;

        #endregion

        #region Unity Classes

        private void FixedUpdate() {
            UpdateSprite();
        }

        #endregion

        #region Class Implementation

        private void UpdateSprite(bool force = false) {
            if (flipbook.IsNull()) {
                return;
            }

            if (flipbook.length == 0) {
                throw new UnityException($"Incorrect flipbook: {requiredSeconds}");
            }

            if (flipbook.fps == 0) {
                throw new UnityException($"Flipbook.fps cannot be less or equal 0: {requiredSeconds}");
            }

            if (!force && Time.time - _lastTime < requiredSeconds) {
                return;
            }

            if (_frame >= flipbook.length) {
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
            UpdateSprite(true);
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