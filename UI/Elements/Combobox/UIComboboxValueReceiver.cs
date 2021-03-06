﻿using System.Collections;
using System.Collections.Generic;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.AssistLib.PropertyReferences;
using UnityEngine;

namespace c1tr00z.AssistLib.GameUI {

    public class UIComboboxValueReceiver : ValueReceiverBase {

        #region Serialized Fields

        [SerializeField] private UICombobox _combobox;
        
        [SerializeField]
        [ReferenceType(typeof(List<>))] 
        private PropertyReference _optionsSrc;
        
        [SerializeField]
        [ReferenceType(typeof(object))] 
        private PropertyReference _selectedValueSrc;

        #endregion

        #region ValueReceiverBase Implementation

        public override IEnumerator<PropertyReference> GetReferences() {
            yield return _optionsSrc;
            yield return _selectedValueSrc;
        }

        public override void UpdateReceiver() {
            _combobox.UpdateCombobox(_optionsSrc.GetList<object>(), _selectedValueSrc.Get<object>());
        }

        #endregion
    }
}