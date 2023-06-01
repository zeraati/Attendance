using Common.Extension;

namespace Domain
{
    public class AttendanceData
    {
        public AttendanceData(string employeeId, string name, string dateTime)
        {
            EmployeeId = employeeId.ToInt();
            Name = name.Trim();
            DateTime = dateTime.ToDateTime();
        }

        public int EmployeeId { get;}
        public string Name { get;}
        public DateTime DateTime { get;}
    }
}
