namespace Application.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Domain.AttendanceData> AttendanceDataRepo { get; }
        IRepository<Domain.AttendanceEmployee> AttendanceEmployeeRepo { get; }

        Task SaveChanges();
    }
}
