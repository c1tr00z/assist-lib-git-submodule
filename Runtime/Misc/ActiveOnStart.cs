using UnityEngine;
using System.Collections;

public class ActiveOnStart : MonoBehaviour {

    #region Serialized Fields

    [SerializeField] private bool _active;

    #endregion

    #region Unity Events

    void Awake() {
	    gameObject.SetActive(_active);
    }

    #endregion
	
}
