using System.Globalization;

namespace Warehouse.WebApi.Helpers
{
    public static class DoubleToSizeStringConverter
    {
        public static string ToPointString(this double? value)
        {
            return value?.ToString("#.00",CultureInfo.GetCultureInfo("en-us"));
        }
    }
}