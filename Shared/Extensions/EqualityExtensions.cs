namespace Shared.Extensions
{
    public static class EqualityExtensions
    {
        public static bool IsEqualTo<T>(this T obj, T other)
        {
            return obj.Equals(other);
        }

        public static bool EqualsIgnoreCase(this string stringA, string stringB)
        {
            return string.Equals(stringA, stringB, StringComparison.OrdinalIgnoreCase);
        }
        public static bool EqualsTrimIgnoreCase(this string propertyName, string comparisonString)
        {
            return propertyName.Trim().Equals(comparisonString, StringComparison.OrdinalIgnoreCase);
        }

        public static bool StartsWithIgnoreCase(this string stringA, string stringB)
        {
            return stringA != null
                   && stringA.StartsWith(stringB, StringComparison.OrdinalIgnoreCase);
        }

        public static int GetHashCode<T>(this T value)
        {
            return value.GetHashCode();
        }

    }
}
