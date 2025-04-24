namespace Shared.Extensions
{
    public static class ListExtensions
    {
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
