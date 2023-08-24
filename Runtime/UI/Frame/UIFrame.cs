using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {
    [RequireComponent(typeof(RectTransform))]
    public class UIFrame : MonoBehaviour, IUIView {

        #region Private Fields

        private UILayer _layer;

        private RectTransform _rectTransform;

        private UIFrameDBEntry _dbEntry;

        #endregion

        #region Accessors

        public UILayer layer => this.GetCachedComponentInParent(ref _layer);

        public RectTransform rectTransform => this.GetCachedComponent(ref _rectTransform);

        public bool isTopFrame => true;// Modules.Get<UI>().IsTopFrameInStack(this);

        public UIFrameDBEntry dbEntry => this.GetDBEntry(ref _dbEntry);

        #endregion

        #region Class Implementation

        public void Show(params object[] args) {
            GetComponentsInChildren<IUIView>().Where(uiView => !uiView.Equals(this)).ToList()
                .ForEach(view => view.Show(args));
        }

        public void Close() {
            layer.Hide(GetComponent<DBEntryResource>().parent as UIFrameDBEntry);
        }

        #endregion
    }
}