using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Queries.Databases
{
    public class GetTestableObjectsQuery : BaseQuery<List<TestableObjectDto>>
    {
        public GetTestableObjectsQuery(string server, int id) 
        {
            Server = server;
            DatabaseId = id;
        }
        public int DatabaseId { get; set; }
        public class GetTestableObjectsQueryHandler : BaseQueryHandler<GetTestableObjectsQuery, List<TestableObjectDto>>
        {
            public GetTestableObjectsQueryHandler(IValidationContextFactory validateDBContextFactory):base(validateDBContextFactory)
            {
            }

            public override async Task<List<TestableObjectDto>> DoTask(GetTestableObjectsQuery request, CancellationToken cancellationToken)
            {
                var database = await ValidateDBContext.GetDatabaseById(request.DatabaseId);
                return await ValidateDBContext.TestableObjectList(database.Name);
            }
        }
    }
}
