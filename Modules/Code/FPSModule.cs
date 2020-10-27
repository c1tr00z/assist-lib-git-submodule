using UnityEngine;

namespace c1tr00z.AssistLib.AppModules {
    public class FPSModule : Module {

        #region Private Fields

        float deltaTime = 0.0f;

        #endregion

        #region Serialized Fields

        [SerializeField] private Color _textColor = Color.magenta;

        #endregion

        #region Unity Events
 
        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }
 
        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;
 
            GUIStyle style = new GUIStyle();
 
            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = _textColor;
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }

        #endregion
    }
}