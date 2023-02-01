using AutoMapper;
using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Queries.Databases
{
    public class GetTestableObjectDetailsByIdQuery : BaseQuery<TestableObjectDetailsDto>
    {
        public int DatabaseId { get; set; }
        public int? ObjectId { get; set; }

        public GetTestableObjectDetailsByIdQuery(string server,int databaseId, int? objectId)
        {
            Server = server;
            DatabaseId = databaseId;
            ObjectId = objectId;
        }

        public class GetTestableObjectDetailsByIdQueryHandler : BaseQueryHandler<GetTestableObjectDetailsByIdQuery, TestableObjectDetailsDto>
        {
            private readonly IMapper _mapper;
            public GetTestableObjectDetailsByIdQueryHandler(IValidationContextFactory validateDBContextFactory, IMapper mapper): base(validateDBContextFactory)
            {
                _mapper = mapper;
            }

            public override async Task<TestableObjectDetailsDto> DoTask(GetTestableObjectDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                var database = await ValidateDBContext.GetDatabaseById(request.DatabaseId);
                try
                {
                    var details = await ValidateDBContext.GetTestableObjectDetailsById(database.Name, request?.ObjectId ?? 0);
                    if (details != null)
                    {
                        var parameters = await ValidateDBContext.GetParametersByDatabaseAndObjectId(database.Name, request?.ObjectId ?? 0);
                        details.Parameters = _mapper.Map<List<BaseParameterDto>>(parameters);
                        details.InputParameters = parameters.Where(p => !p.IsOutput).ToList();
                        details.OutputParameters = details.InitializeOutputParameters(_mapper);

                    }
                    return details ?? new TestableObjectDetailsDto();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return new TestableObjectDetailsDto();
                }
                
            }
        }
    }
}
