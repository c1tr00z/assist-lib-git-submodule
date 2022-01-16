using UnityEngine;

namespace c1tr00z.AssistLib.Common.Coroutines {
    public class CoroutineRequest : CustomYieldInstruction {

        #region Nested Classes

        public enum State {
            NotStarted,
            InProgress,
            Done,
        }

        #endregion

        #region Private Fields

        private State _state = State.NotStarted;

        #endregion

        #region Accessors

        public State state {
            get => _state;
            private set => _state = value;
        }

        #endregion

        #region CustomYieldInstruction Implementation

        public override bool keepWaiting {
            get {
                if (state == State.NotStarted) {
                    state = State.InProgress;
                }

                return state != State.Done;
            }
        }

        #endregion

        #region Class Implementation

        public void Finish() {
            state = State.Done;
        }

        public static CoroutineRequest MakeFinishedRequest() {
            var request = new CoroutineRequest();
            request.Finish();
            return request;
        }

        #endregion
    }
}