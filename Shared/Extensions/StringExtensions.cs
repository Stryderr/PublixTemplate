namespace Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualTo(this string propertyName, string comparisonString)
        {
            return propertyName.Equals(comparisonString, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualToTrim(this string propertyName, string comparisonString)
        {
            return propertyName.Trim().Equals(comparisonString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
