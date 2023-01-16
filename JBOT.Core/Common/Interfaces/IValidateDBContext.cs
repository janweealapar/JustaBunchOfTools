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
        Task<List<DatabaseDto>> DatabaseList { get; }
        Task<DatabaseDto> GetDatabaseById(int id);
        Task<List<TestableObjectDto>> TestableObjectList(string databaseName);
        Task<TestableObjectDetailsDto> GetTestableObjectDetailsById(string databaseName, int objectId);
        Task<List<ParameterDto>> GetParametersByDatabaseAndObjectId(string databaseName, int objectId);
        Task<TestableObjectDetailsDto> RunUnitTest(TestableObjectDetailsDto unitTest);
    }
}
