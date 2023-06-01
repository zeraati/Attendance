using Persistance;
using Application.Interface;
using Infrastructure.Inventory;
using Application.Attendance.Query;
using Application.AttendanceData.Query;
using Application.AttendanceEmployee.Command.CreateRange;


IUnitOfWork _unitOfWork = new UnitOfWork();
IInventoryService _inventory = new InventoryService();
IAllAttendanceData _attendanceData = new AllAttendanceData(_unitOfWork);
ICreateAttendanceEmployee _createAttendanceEmployee = new CreateAttendanceEmployee(_unitOfWork, _inventory);

var attendanceData = await _attendanceData.Execute();

var model = new CreateAttendanceEmployeeModel { Attendances = attendanceData };
await _createAttendanceEmployee.Execute(model);
