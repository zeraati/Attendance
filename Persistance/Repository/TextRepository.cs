using Domain;
using Application.Interface;

namespace Persistance.Repository
{
    public class TextRepository: IRepository<AttendanceData>
    {
        private string _filePath;
        public TextRepository(string filePath)=> _filePath = filePath;

        public Task Add(AttendanceData entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<AttendanceData> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AttendanceData>> All()
        {
            var result = new List<AttendanceData>();
            try
            {
                var lines = await File.ReadAllLinesAsync(_filePath);

                foreach (var line in lines)
                {
                    var data = line.Split("\t");

                    if (data.Length != 3) throw new Exception(Common.Resource.fa_IR.InputDataShouldIncludeThreeColumns_EmployeeId_EmployeeName_RecordTime_);

                    var employeeAttendance = new AttendanceData(
                        employeeId: data[0],
                        name: data[1],
                        dateTime: data[2]);

                    result.Add(employeeAttendance);
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }

            return result;
        }
    }
}
