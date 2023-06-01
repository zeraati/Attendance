namespace Common.Extension
{
    public static class TimeSpanExtension
    {
        public static string ToStringHHMM(this TimeSpan param)
        {
            if (param == TimeSpan.Zero) return "";

            var result = $"{param.Hours}:{param.Minutes}";
            return result;
        }
    }
}
