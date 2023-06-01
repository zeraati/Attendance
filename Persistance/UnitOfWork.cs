using Domain;
using Application.Interface;
using Persistance.Repository;

namespace Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<AttendanceData> _attendanceDataRepo;
        public IRepository<AttendanceData> AttendanceDataRepo
        {
            get
            {
                if (_attendanceDataRepo == null) _attendanceDataRepo = new TextRepository(Setting.Input);
                return _attendanceDataRepo;
            }
        }

        private IRepository<AttendanceEmployee> _attendanceEmployeeRepo;
        public IRepository<AttendanceEmployee> AttendanceEmployeeRepo
        {
            get
            {
                if (_attendanceEmployeeRepo == null) _attendanceEmployeeRepo = new CSVRepository(Setting.Output);
                return _attendanceEmployeeRepo;
            }
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
