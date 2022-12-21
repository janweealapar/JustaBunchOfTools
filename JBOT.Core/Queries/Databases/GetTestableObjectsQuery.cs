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
    public class GetTestableObjectsQuery : IRequest<List<TestableObjectDto>>
    {
        public GetTestableObjectsQuery(int id) 
        {
            DatabaseId = id;
        }
        public int DatabaseId { get; set; }
        public class GetTestableObjectsQueryHandler : IRequestHandler<GetTestableObjectsQuery, List<TestableObjectDto>>
        {
            private readonly IValidateDBContext _validateDBContext;

            public GetTestableObjectsQueryHandler(IValidateDBContext validateDBContext)
            {
                _validateDBContext = validateDBContext;
            }
            public async Task<List<TestableObjectDto>> Handle(GetTestableObjectsQuery request, CancellationToken cancellationToken)
            {
                var database = await _validateDBContext.GetDatabaseById(request.DatabaseId);
                return await _validateDBContext.TestableObjectList(database.Name);
            }
        }
    }
}
