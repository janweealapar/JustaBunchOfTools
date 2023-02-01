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
    public class GetAllDatabasesQuery: BaseQuery<List<DatabaseDto>>
    {
        public GetAllDatabasesQuery(string server)
        {
            base.Server = server;
        }
        public class GetAllDatabasesQueryHandler : BaseQueryHandler<GetAllDatabasesQuery, List<DatabaseDto>>
        {
            public GetAllDatabasesQueryHandler(IValidationContextFactory validateDBContextFactory):base(validateDBContextFactory)
            {
            }

            public override async Task<List<DatabaseDto>> DoTask(GetAllDatabasesQuery request, CancellationToken cancellationToken)
            {
                return await ValidateDBContext.DatabaseList;
            }
        }
    }
}
