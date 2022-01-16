using UnityEngine;

namespace c1tr00z.AssistLib.Utils {
    public class PrefabSpawner : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private GameObject _prefabSrc;

        #endregion

        #region Unity Events

        private void Awake() {
            var clone = _prefabSrc.Clone();
            clone.name = _prefabSrc.name;
            clone.transform.SetParent(transform.parent);
            clone.transform.localPosition = transform.localPosition;
            clone.transform.localRotation = transform.localRotation;
            clone.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }

        #endregion

    }
}
