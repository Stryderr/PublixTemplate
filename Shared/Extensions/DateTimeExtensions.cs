namespace Shared.Extensions
{
    public static class DateTimeExtensions
    {

        public static bool IsBusinessDay(DateTime date, HashSet<DateTime> holidays)
        {
            return date.DayOfWeek != DayOfWeek.Saturday &&
                   date.DayOfWeek != DayOfWeek.Sunday &&
                   holidays.All(x => x.Date != date.Date);
        }
    }
}
