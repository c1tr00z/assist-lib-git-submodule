using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.AssistLib.Sprites {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {

        #region Nested Classes

        [Serializable]
        private class StateEvent : UnityEvent<string> {
        }

        #endregion

        #region Events

        public event Action<string> OnStateEvent; 

        #endregion

        #region Private Fields

        private SpriteAnimatorState _curretState;

        private SpriteRenderer _spriteRenderer;

        private float _lastTime;

        private float _requiredSeconds;

        #endregion

        #region Serialized Fields

        [SerializeField] private string defaultStateName;
        [SerializeField] private List<SpriteAnimatorState> _states = new List<SpriteAnimatorState>();
        [SerializeField] private StateEvent _onStateEvent;
        [SerializeField] private int _fps = 12;

        #endregion

        #region Accessors

        public SpriteAnimatorState currentState {
            get {
                if (_curretState == null) {
                    _curretState = GetState(defaultStateName);
                    _curretState.Activate();
                }

                return _curretState;
            }
            set {
                _curretState = value;
                _curretState.Activate();
            }
        }

        public SpriteRenderer spriteRenderer => this.GetCachedComponent(ref _spriteRenderer);

        #endregion

        #region Unity Events

        private void LateUpdate() {
            FixRotation();
        }

        private void FixedUpdate() {
            Play();
        }

        #endregion

        #region Class Implementation

        private void Play() {

            if (_fps <= 0) {
                return;
            }
            
            if (Time.time - _lastTime < _requiredSeconds) {
                return;
            }

            var frameInfo = currentState.NextFrame();

            spriteRenderer.sprite = frameInfo.sprite;
            
            frameInfo.events.ForEach(OnEvent);

            if (!string.IsNullOrEmpty(frameInfo.nextStateName)) {
                currentState = GetState(frameInfo.nextStateName);
            }
            
            _requiredSeconds = 1f / _fps;
            _lastTime = Time.time;
        }

        private SpriteAnimatorState GetState(string stateName) {
            return _states.FirstOrDefault(s => s.name == stateName);
        }

        private void OnEvent(SpriteAnimatorStateEvent stateEvent) {
            if (_onStateEvent != null) {
                _onStateEvent.Invoke(stateEvent.eventName);
            }
            OnStateEvent?.Invoke(stateEvent.eventName);
        }

        private void FixRotation() {
            if (transform.parent == null) {
                return;
            }

            var angles = transform.parent.rotation.eulerAngles;
            angles.x = -angles.x;
            angles.y = -angles.y;
            angles.z = -angles.z;
            transform.localRotation = Quaternion.Inverse(transform.parent.rotation);
        }

        #endregion
    }
}