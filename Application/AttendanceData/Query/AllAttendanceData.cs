using Application.Interface;
using Application.Attendance.Query;

namespace Application.AttendanceData.Query
{
    public class AllAttendanceData : IAllAttendanceData
    {
        private readonly IUnitOfWork _unitOfWork;
        public AllAttendanceData(IUnitOfWork unitOfWork)=> _unitOfWork = unitOfWork;

        async Task<IEnumerable<Domain.AttendanceData>> IAllAttendanceData.Execute()
        {
            var result = await _unitOfWork.AttendanceDataRepo.All();
            return result;
        }
    }
}
