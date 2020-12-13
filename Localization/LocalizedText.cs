using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.Localization {
    [RequireComponent(typeof(Text))]
    public abstract class LocalizedText : MonoBehaviour {

        #region Serialized Fields

        [SerializeField]
        protected string key = "Title";

        #endregion

        #region Unity Events

        private void Awake() {
            Localize();
        }

        #endregion

        #region Class Implementation

        public void Localize() {
            GetComponent<Text>().text = GetLocalizedText();
        }

        #endregion

        #region Abstract Methods

        protected abstract string GetLocalizedText();

        #endregion
    }
}