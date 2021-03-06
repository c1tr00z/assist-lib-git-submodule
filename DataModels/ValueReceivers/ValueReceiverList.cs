﻿using System.Collections.Generic;
using c1tr00z.AssistLib.PropertyReferences;
using c1tr00z.AssistLib.GameUI;
using UnityEngine;

namespace c1tr00z.AssistLib.DataModels {

    public class ValueReceiverList : ValueReceiverBase {

        #region Serialized Fields

        [ReferenceTypeAttribute(typeof(List<>))]
        [SerializeField]
        private PropertyReference _listSource;

        [SerializeField] private UIList _list;

        #endregion

        #region ValueReceiverBase

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _listSource;
        }

        public override void UpdateReceiver() {
            _list.UpdateList(_listSource.GetList<object>());
        }

        #endregion
    }
}

