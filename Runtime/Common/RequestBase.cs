using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public class RequestBase : CustomYieldInstruction {

        #region Nested Classes

        public enum State {
            InProgress,
            Done
        }

        #endregion

        #region Accessors

        public State state { get; protected set; } = State.InProgress;

        public bool isDone => state == State.Done;

        #endregion
        
        #region CustomYieldInstruction Implementation

        public override bool keepWaiting => !isDone;

        #endregion

        #region Class Implementation

        public void Finish() {
            state = State.Done;
        }

        #endregion
    }
}