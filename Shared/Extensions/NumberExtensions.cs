namespace Shared.Extensions
{
    public static class NumericalExtensions
    {
        public static int ToInt(this string value, int defaultValue = 0)
        {
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        public static int? ToNullableInt(this string value, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToInt();
        }

        public static long ToLong(this string value, int defaultValue = 0)
        {
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        public static long? ToNullableLong(this string value, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToInt();
        }

        public static bool ToBool(this string value)
        {
            return bool.TryParse(value, out bool result) && result;
        }

        public static decimal ToDecimal(this string value, decimal defaultValue = 0m)
        {
            return decimal.TryParse(value, out var result) ? result : defaultValue;
        }

        public static decimal? ToNullableDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToDecimal();
        }


    }
}
