using Common.Resource;

namespace Common.Utility
{
    public class Utility
    {
        public static IEnumerable<string> GetPropertiesNam(Type type,bool localized=true,List<string>? exclude=null)
        {
            var result = new List<string>();
            foreach (var property in type.GetProperties())
            {
                if (exclude != null && exclude.Any(x => x == property.Name)) continue;

                if (localized==true)
                {
                    var resource = fa_IR.ResourceManager.GetString(property.Name);

                    if (resource != null) result.Add(resource);
                    else result.Add(property.Name);
                }
                else result.Add(property.Name);
            }

            return result;
        }
    }
}
