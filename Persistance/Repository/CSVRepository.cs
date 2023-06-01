using Domain;
using System.Text;
using Common.Utility;
using Common.Resource;
using Common.Extension;
using Application.Interface;
using Domain.Common;

namespace Persistance.Repository
{
    public class CSVRepository : IRepository<AttendanceEmployee>
    {
        private string _filePath;
        public CSVRepository(string filePath) => _filePath = filePath;

        public Task Add(AttendanceEmployee entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRange(IEnumerable<AttendanceEmployee> entities)
        {
            var data = new List<string>();

            var exclude=new List<string> { "Id", "EmployeeId", "CountRecords" };
            var head = Utility.GetPropertiesNam(typeof(AttendanceEmployee),true, exclude).ToList().StringJoin();
            head = fa_IR.ResourceManager.GetString("RowNum")+","+head;
            data.Add(head);

            var rowNum = 0;
            foreach (var entity in entities)
            {
                rowNum++;
                var otherTimes = entity.OtherTimes.Any() ?entity.OtherTimes.Select(x => x.ToStringHHMM()).ToList().StringJoin(" | "):"";
                var items = new List<string>{
                    rowNum.ToString(),
                    entity.Date,
                    entity.DayName,
                    entity.Name,
                    fa_IR.ResourceManager.GetString("Type"+entity.Type.ToString()),
                    entity.WorkTime.GetValueOrDefault().ToStringHHMM(),
                    entity.Entry.GetValueOrDefault().ToStringHHMM(),
                    entity.Exit.GetValueOrDefault().ToStringHHMM(),
                    otherTimes,
                    entity.Note
                };

                data.Add(items.StringJoin());
            }

            try
            {
                await File.WriteAllLinesAsync(_filePath, data, Encoding.UTF8);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        public Task<IEnumerable<AttendanceEmployee>> All()
        {
            throw new NotImplementedException();
        }

    }
}
