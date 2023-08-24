using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.SceneManagement;

namespace c1tr00z.AssistLib.LoadingScreen {
    public class UIViewLoadingScreen : UIView {

        #region Accessors

        public float loadingProgress { get; private set; }

        #endregion

        #region Unity Events

        private void OnEnable() {
            LoadingScreenController.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDisable() {
            LoadingScreenController.ProgressUpdated -= OnProgressUpdated;
        }

        #endregion

        #region UIView Implementation

        protected override void OnShow(params object[] args) {
            loadingProgress = 0;
            base.OnShow(args);
        }

        #endregion

        #region Class Implementation

        private void OnProgressUpdated(float progress) {
            loadingProgress = progress;
            OnDataChanged();
        }

        #endregion
    }
}