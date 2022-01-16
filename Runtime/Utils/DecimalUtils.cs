using System;
using System.Globalization;
using UnityEngine;

public static class DecimalUtils {

    #region Class Implementation

    public static string ToInvariantCultureString(this float floatValue) {
        return floatValue.ToString("G", CultureInfo.InvariantCulture);
    }

    public static float Round(float value, int coefficient = 1) {
        return Mathf.Round(value * coefficient) / coefficient;
    }

    #endregion
}
