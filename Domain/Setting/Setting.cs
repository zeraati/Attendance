using Common.Extension;
using Newtonsoft.Json.Linq;

namespace Domain
{
    public class Setting
    {
        static Setting()
        {
            var setting = File.ReadAllText("Setting.json");
            var jObj = JObject.Parse(setting);

            var workTimes = jObj["WorkTimes"];
            StartWork = workTimes["Start"].ToString().ToTimeSpan();
            WorkTimeTolerance =("00:"+workTimes["Tolerance"].ToString()).ToTimeSpan();
            WorkTime = workTimes["WorkTime"].ToString().ToTimeSpan();

            var data= jObj["Data"];
            Input = data["Input"].ToString();
            Output = data["Output"].ToString();
        }

        public static TimeSpan StartWork { get; }
        public static TimeSpan WorkTimeTolerance { get; }
        public static TimeSpan WorkTime { get; }

        public static string Input { get; }
        public static string Output { get; }
    }
}
