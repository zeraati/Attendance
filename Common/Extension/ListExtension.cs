namespace Common.Extension
{
    public static class ListExtension
    {
        public static string StringJoin<T>(this List<T> param,string sep=",")=>string.Join(sep,param);
    }
}