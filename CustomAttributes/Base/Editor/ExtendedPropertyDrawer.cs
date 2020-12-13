using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.CustomAttributes {
    public abstract class ExtendedPropertyDrawer : PropertyDrawer {

        #region Protected Fields

        protected Rect position;
    
        protected SerializedProperty _currentProperty;

        #endregion

        #region Class Implementation

        public void AddX(float width) {
            position.xMin += width;
        }

        public float width {
            get { return position.width; }
        }

        public float propertyNameWidth {
            get { return EditorGUIUtils.GetDisplayNameFieldWidth(width); }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            _currentProperty = property;
            this.position = position;
            DrawPropertyName();
            AddX(EditorGUIUtils.GetDisplayNameFieldWidth(width));
            Show(property);
            _currentProperty = null;
        }

        public abstract void Show(SerializedProperty property);

        protected void DrawPropertyName() {
            if (_currentProperty == null) {
                return;
            }
            EditorGUI.PrefixLabel(position, new GUIContent(_currentProperty.displayName));
        }

        #endregion
    }
}
