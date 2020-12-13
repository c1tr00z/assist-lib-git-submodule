using System.Globalization;

public static class DecimalUtils {

    #region Class Implementation

    public static string ToInvariantCultureString(this float floatValue) {
        return floatValue.ToString("G", CultureInfo.InvariantCulture);
    }

    #endregion
}
