using System.Globalization;

namespace Common.Extension
{
    public static class DateTimeExtension
    {
        public static string PersianDate(this DateTime param)
        {
            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(param);
            var month = persianCalendar.GetMonth(param);
            var day = persianCalendar.GetDayOfMonth(param);

            var result = $"{year}/{month.ToString("D2")}/{day.ToString("D2")}";
            return result;
        }

        public static DateTime ToDateTime(this DateOnly param)
        {
            var result = param.ToDateTime(TimeOnly.MinValue);
            return result;
        }

        public static string PersianDayName(this DateTime param)
        {
            var result = "";
            switch (param.DayOfWeek)
            {
                case DayOfWeek.Saturday: result = "شنبه"; break;
                case DayOfWeek.Sunday: result = "یکشنبه"; break;
                case DayOfWeek.Monday: result = "دوشنبه"; break;
                case DayOfWeek.Tuesday: result = "سه شنبه"; break;
                case DayOfWeek.Wednesday: result = "چهارشنبه"; break;
                case DayOfWeek.Thursday: result = "پنجشنبه"; break;
                case DayOfWeek.Friday: result = "جمعه"; break;
            }

            return result;
        }

        public static DateOnly ToDateOnly(this DateTime param)
        {
            if (param==DateTime.MinValue) return default;

            var result = DateOnly.FromDateTime(param);
            return result;
        }
    }
}
