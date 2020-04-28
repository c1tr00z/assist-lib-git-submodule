using UnityEngine;

namespace c1tr00z.AssistLib.UI {
    [RequireComponent(typeof(RectTransform))]
    public class UIFrame : MonoBehaviour, IUIView {

        private UILayerBase _layer;

        private RectTransform _rectTransform;

        public UILayerBase layer => this.GetCachedComponentInParent(ref _layer);

        public RectTransform rectTransform => this.GetCachedComponent(ref _rectTransform);

        public bool isTopFrame => UI.instance.IsTopFrameInStack(this);

        public void Show(params object[] args) {
            GetComponentsInChildren<IUIView>().Where(uiView => !uiView.Equals(this))
                .ForEach(view => view.Show(args));
        }

        public void Close() {
            layer.Close(GetComponent<DBEntryResource>().parent as UIFrameDBEntry);
        }
    }
}