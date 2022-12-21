using JBOT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Common.Interfaces
{
    public interface IValidateDBContext
    {
        public Task<List<DatabaseDto>> DatabaseList { get; }
        public Task<DatabaseDto> GetDatabaseById(int id);
        public Task<List<TestableObjectDto>> TestableObjectList(string databaseName);
    }
}
