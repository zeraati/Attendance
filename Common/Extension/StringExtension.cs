using System.Globalization;

namespace Common.Extension
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string param)=>string.IsNullOrEmpty(param);

        public static int ToInt(this string param)
        {
            if (param.IsEmpty()) return default;

            _ = int.TryParse(param, out var result);
            return result;
        }

        public static DateTime ToDateTime(this string param)
        {
            if (param.IsEmpty()) return default;

            _ = DateTime.TryParse(param, out var result);
            return result;
        }

        public static DateTime PersianDateToDateTime(this string param)
        {
            if (param.IsEmpty()) return default;

            var split=param.Split('-');
            if(split.Length!=3) param.Split('/');
            if(split.Length!=3) param.Split('\\');

            if(split.Length!=3) return default;

            var result = new DateTime(split[0].ToInt(), split[1].ToInt(), split[2].ToInt(), new PersianCalendar());
            return result;
        }

        public static TimeSpan ToTimeSpan(this string param)
        {
            if (param.IsEmpty()) return default;

            _ = TimeSpan.TryParse(param, out var result);
            return result;
        }
    }
}