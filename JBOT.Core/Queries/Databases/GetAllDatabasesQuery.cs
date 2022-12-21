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
    public class GetAllDatabasesQuery: IRequest<List<DatabaseDto>>
    {
        public class GetAllDatabasesQueryHandler : IRequestHandler<GetAllDatabasesQuery, List<DatabaseDto>>
        {
            private readonly IValidateDBContext _validateDBContext;

            public GetAllDatabasesQueryHandler(IValidateDBContext validateDBContext)
            {
                _validateDBContext = validateDBContext;
            }
            public async Task<List<DatabaseDto>> Handle(GetAllDatabasesQuery request, CancellationToken cancellationToken)
            {
                return await _validateDBContext.DatabaseList;
            }
        }
    }
}
