namespace c1tr00z.AssistLib.EditorTools {
    public class EditorToolName : System.Attribute {

        #region Public Fields

        public string toolName;

        #endregion

        #region Constructors

        public EditorToolName(string toolName) {
            this.toolName = toolName;
        }

        #endregion

        #region Attribute Implementation

        public override bool Match(object obj) {
            var other = obj as EditorToolName;
            if (other == null) {
                return false;
            }

            return other.toolName == this.toolName;
        }

        #endregion
    }
}
