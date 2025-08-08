using UnityEngine;

namespace c1tr00z.AssistLib.FolderReference {
    public class FolderReferenceAttribute : PropertyAttribute {
        #region Accessors
        
        public bool isFolder { get; }

        #endregion

        #region Constructors

        public FolderReferenceAttribute(bool isFolder = false) {
            this.isFolder = isFolder;
        }

        #endregion
    }
}