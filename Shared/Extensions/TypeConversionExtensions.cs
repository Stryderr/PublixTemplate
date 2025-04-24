namespace Shared.Extensions
{
    public static class TypeConversionExtensions
    {
        public static int ToInt(this string s)
        {
            int.TryParse(s, out int result);
            return result;
        }

        public static bool ToBool(this string value)
        {
            return bool.TryParse(value, out bool result) && result;
        }

        public static void Separate<T>(this List<T> source, Func<T, bool> condition, out List<T> list1, out List<T> list2)
        {
            list1 = new List<T>();
            list2 = new List<T>();

            foreach (var item in source)
            {
                if (condition(item))
                {
                    list1.Add(item);
                }
                else
                {
                    list2.Add(item);
                }
            }
        }
    }
}
