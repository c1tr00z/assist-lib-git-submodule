using System.Collections.Generic;

namespace AssistLib.Editor.Utils {
    public class EditorGUIListData<T> {

        #region Public Fields

        public string label = null;

        public List<T> items = new List<T>();

        public bool showList = true;

        public bool allowSceneObjects = false;

        #endregion

        #region Constructors

        public EditorGUIListData(bool allowSceneObjects = false) {
            items = new List<T>();
            showList = true;
            this.allowSceneObjects = allowSceneObjects;
        }

        public EditorGUIListData(string label, bool allowSceneObjects = false) {
            this.label = label;
            items = new List<T>();
            showList = true;
            this.allowSceneObjects = allowSceneObjects;
        }

        #endregion

    }
}