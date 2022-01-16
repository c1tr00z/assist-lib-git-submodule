using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace c1tr00z.AssistLib.GameUI {
    [ExecuteInEditMode]
    public class UIRadialLayoutGroup : LayoutGroup {

        #region Public Fields

        public int radius = 100;

        public int angleDelta = 90;

        public int step = 10;

        #endregion

        #region LayoutGroup Implementation

        public override void CalculateLayoutInputVertical() {
            Refresh();
        }

        public override void SetLayoutHorizontal() {
            Refresh();
        }

        public override void SetLayoutVertical() {
            Refresh();
        }

        #endregion

        #region Class Implementation

        protected List<RectTransform> GetChildren() {
            return transform.GetChildren().Where(c => {
                var layoutElement = c.GetComponent<LayoutElement>();
                return layoutElement == null || !layoutElement.ignoreLayout;
            }).Select(c => c as RectTransform).ToList();
        }

        protected Vector2 GetCoordsByAngleAndRadius(float angle, float radius) {
            return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, Mathf.Sin(Mathf.Deg2Rad * angle) * radius);
        }

        protected void SetPositionByAngleAndRadius(RectTransform transform, float angle, bool setAngle) {
            var trueAngle = angle + angleDelta;
            transform.anchoredPosition = GetCoordsByAngleAndRadius(angle + angleDelta, radius);
            if (setAngle) {
                transform.eulerAngles = new Vector3(0, 0, trueAngle - angleDelta);
            }
        }

        protected virtual void Refresh() {
            var children = GetChildren();

            var count = children.Count;
            
            var firstAngle = (children.Count - 1) * (-1 * step / 2);
            for (var i = 0; i < children.Count; i++) {
                var child = children[i];
                var angle = firstAngle + (i * step);
                SetPositionByAngleAndRadius(child, angle, true);
            }
        }

        #endregion
    }
}
