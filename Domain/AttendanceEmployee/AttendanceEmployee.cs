using Common.Enum;
using Common.Resource;
using Common.Extension;
using Domain.Common;

namespace Domain
{
    public class AttendanceEmployee : IEntity<long>
    {
        public AttendanceEmployee(DateOnly day, int employeeId, string name, IEnumerable<AttendanceData> attendances)
        {
            Date = day.ToDateTime().PersianDate();
            DayName = day.ToDateTime().PersianDayName();
            EmployeeId = employeeId;
            Name = name;

            Attendances = attendances.ToList();
            CountRecords = Attendances.Count;

            if (CountRecords == 0) Type = Types.DailyLeave;//رکوردی ورجود ندارد
            else if (CountRecords.IsOdd())//تمامی ورود و خروج ها ثبت نشده اند
            {
                Type = Types.Error;
                Note = fa_IR.AllEntryExitAreNotRecorded;
            }
            else//رکورد ورود و خروج صحیح وجود دارد
            {
                SetEntry();
                SetExit();
                SetOtherTimes();
                SetStartEndWorkTime();
                SetWorkTime();
                SetType();
            }
        }

        public long Id { get; }
        public string Date { get; }
        public string DayName { get; }
        public int EmployeeId { get; }
        public string Name { get; }

        public Types Type { get; private set; } = Types.Normal;

        public TimeSpan? WorkTime { get; private set; }
        public TimeSpan? Entry { get; private set; }
        public TimeSpan? Exit { get; private set; }
        public List<TimeSpan> OtherTimes { get; private set; } = new List<TimeSpan>();

        public string Note { get; }
        public int CountRecords { get; }

        private List<AttendanceData> Attendances { get; }
        private TimeSpan StartWorkTime{ get; set; }
        private TimeSpan EndWorkTime{ get; set; }

        private void SetEntry() => Entry = Attendances.Select(x => x.DateTime.TimeOfDay).FirstOrDefault();
        private void SetExit() => Exit = Attendances.Select(x => x.DateTime.TimeOfDay).LastOrDefault();
        private void SetOtherTimes()
            => OtherTimes = Attendances.Where(x => x.DateTime.TimeOfDay != Entry && x.DateTime.TimeOfDay != Exit).Select(x => x.DateTime.TimeOfDay).ToList();

        private void SetStartEndWorkTime()
        {
            StartWorkTime = Entry < Setting.StartWork ? Setting.StartWork : Entry.GetValueOrDefault();
            EndWorkTime = StartWorkTime+Setting.WorkTime;
        }

        private void SetWorkTime()
        {
            WorkTime = TimeSpan.Zero;
            for (var i = 0; i < CountRecords; i += 2)
            {
                var entry = Attendances[i].DateTime.TimeOfDay;
                var exit = Attendances[i + 1].DateTime.TimeOfDay;

                var startWork = entry;
                var endWork = exit;

                //ورود زود هنگام در کارکرد روزانه محسابه نمی شود
                if (startWork < StartWorkTime) startWork = StartWorkTime;

                //خروج دیر هنگام در کارکرد روزانه محسابه نمی شود
                if (endWork > EndWorkTime) endWork = EndWorkTime;

                WorkTime += endWork - startWork;
            }
        }

        private void SetType()
        {
            if (WorkTime < Setting.WorkTime) Type = Types.Delay;//ساعت کاری کامل نیست

            //تاخیر در ورود
            var entryLate = Entry > StartWorkTime ? Entry - StartWorkTime : TimeSpan.Zero;
            if (CountRecords > 2 && Type == Types.Delay)//بیش از ۲ رکورد وجود دارد و ساعت کاری کامل نیست
            {
                //تاخیر در ورود نداشته اما ساعت کارکرد کمتر از ساعت کاری اجباری است
                if (entryLate == TimeSpan.Zero) Type = Types.HourlyLeave;

                //تاخیر در ورود داشته و جمع تاخیر و کارکرد بازهم کمتر از ساعت کاری اجباری است
                else if (entryLate + WorkTime < Setting.WorkTime) Type = Types.DelayAndHourlyLeave;
            }
        }
    }
}
