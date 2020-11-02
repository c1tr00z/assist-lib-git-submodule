using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.AssistLib.Sprites {
    [RequireComponent(typeof(Rigidbody))]
    public class Rigidbody4DirectionsPrefixSetter : SpriteAnimatorPrefixSetter {

        #region Nested Classes

        private enum Directions {
            Left,
            Right,
            Up,
            Down
        }

        [Serializable]
        private class DirectionPrefix {
            public Directions direction;
            public string prefix;
        }

        #endregion

        #region Private Fields

        private Directions _lastDirection = Directions.Right;

        private Rigidbody _rigidbody;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private List<DirectionPrefix> _directions = new List<DirectionPrefix>();

        // [SerializeField] private string _moveStateName;

        // [SerializeField] private float _speed = 1;

        // [SerializeField] private KeyCode _upKey;
        //
        // [SerializeField] private KeyCode _downKey;
        //
        // [SerializeField] private KeyCode _rightKey;
        //
        // [SerializeField] private KeyCode _leftKey;
        
        #endregion

        #region Accessors

        public Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);

        #endregion
        
        #region Unity Events

        private void Start() {
            RefreshPrefix(_lastDirection);
        }

        private void LateUpdate() {
            SetPrefix();
        }

        #endregion

        #region Class Implemenetation

        private void SetPrefix() {

            var rigidbodyVelocity = rigidbody.velocity;
            
            var direction = _lastDirection;

            if (rigidbodyVelocity.magnitude > 0) {
                var isLeftRight = Mathf.Abs(rigidbodyVelocity.x) > Mathf.Abs(rigidbodyVelocity.z);

                direction = isLeftRight
                    ? rigidbodyVelocity.x > 0
                        ? Directions.Right
                        : Directions.Left
                    : rigidbodyVelocity.z > 0
                        ? Directions.Up
                        : Directions.Down;
                
                RefreshPrefix(direction);
            }
        }

        // public void Update() {
        //     var velocity = new Vector3();
        //     if (Input.GetKey(_upKey)) {
        //         velocity.z += _speed;
        //     }
        //     if (Input.GetKey(_downKey)) {
        //         velocity.z -= _speed;
        //     }
        //     if (Input.GetKey(_rightKey)) {
        //         velocity.x += _speed;
        //     }
        //     if (Input.GetKey(_leftKey)) {
        //         velocity.x -= _speed;
        //     }
        //
        //     rigidbody.velocity = velocity;
        // }

        private void RefreshPrefix(Directions direction) {
            currentPrefix = _directions.FirstOrDefault(d => d.direction == direction)?.prefix;
            _lastDirection = direction;
        }

        #endregion
    }
}