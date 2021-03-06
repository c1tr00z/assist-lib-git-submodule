﻿using UnityEngine;

namespace c1tr00z.AssistLib.Localization {
    public class LocalizedTextSimple : LocalizedText {

        #region LocalizedText Implementation

        protected override string GetLocalizedText() {
            return Localization.Translate(key);
        }

        #endregion

    }
}
