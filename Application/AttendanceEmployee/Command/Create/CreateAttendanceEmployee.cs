using Common.Extension;
using Application.Interface;

namespace Application.AttendanceEmployee.Command.CreateRange
{
    public class CreateAttendanceEmployee : ICreateAttendanceEmployee
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryService _inventory;
        public CreateAttendanceEmployee(IUnitOfWork unitOfWork, IInventoryService inventory)
        {
            _inventory = inventory;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(CreateAttendanceEmployeeModel model)
        {
            var setting = new Domain.Setting();
            var data = await _unitOfWork.AttendanceDataRepo.All();

            var days = data.Select(x => x.DateTime.Date.ToDateOnly()).Distinct().ToList();
            var employees = data.Select(x => new { Id = x.EmployeeId, x.Name }).DistinctBy(x => x.Id).ToList();


            var attendanceEmployees = new List<Domain.AttendanceEmployee>();
            foreach (var day in days)
            {
                foreach (var employee in employees)
                {
                    //دیتای پایه روز جاری براساس کارمند
                    var dayAttendances = data.Where(x => x.EmployeeId == employee.Id && x.DateTime.Date == day.ToDateTime().Date).ToList();

                    var attendanceEmployee = new Domain.AttendanceEmployee(day, employee.Id, employee.Name, dayAttendances);
                    attendanceEmployees.Add(attendanceEmployee);
                }
            }

            await _unitOfWork.AttendanceEmployeeRepo.AddRange(attendanceEmployees);

            _inventory.NotifySaleOcurred();
        }
    }
}
