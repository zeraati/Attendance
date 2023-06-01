namespace Application.AttendanceEmployee.Command.CreateRange
{
    public interface ICreateAttendanceEmployee
    {
        Task Execute(CreateAttendanceEmployeeModel model);
    }
}
