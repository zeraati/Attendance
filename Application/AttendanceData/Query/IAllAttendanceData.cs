namespace Application.Attendance.Query
{
    public interface IAllAttendanceData
    {
       Task<IEnumerable<Domain.AttendanceData>> Execute();
    }
}
