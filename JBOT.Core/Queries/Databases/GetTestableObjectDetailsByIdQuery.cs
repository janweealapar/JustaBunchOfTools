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
    public class GetTestableObjectDetailsByIdQuery : IRequest<TestableObjectDetailsDto>
    {
        public int DatabaseId { get; set; }
        public int? ObjectId { get; set; }

        public GetTestableObjectDetailsByIdQuery(int databaseId, int? objectId)
        {
            DatabaseId = databaseId;
            ObjectId = objectId;
        }

        public class GetTestableObjectDetailsByIdQueryHandler : IRequestHandler<GetTestableObjectDetailsByIdQuery, TestableObjectDetailsDto>
        {
            private readonly IValidateDBContext _validateDBContext;
            private readonly IMapper _mapper;
            public GetTestableObjectDetailsByIdQueryHandler(IValidateDBContext validateDBContext, IMapper mapper)
            {
                _validateDBContext = validateDBContext;
                _mapper = mapper;
            }

            public async Task<TestableObjectDetailsDto> Handle(GetTestableObjectDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                var database = await _validateDBContext.GetDatabaseById(request.DatabaseId);
                var details = await _validateDBContext.GetTestableObjectDetailsById(database.Name, request?.ObjectId ?? 0);
                if (details != null) 
                { 
                    var parameters = await _validateDBContext.GetParametersByDatabaseAndObjectId(database.Name, request?.ObjectId ?? 0);
                    details.Parameters = _mapper.Map<List<BaseParameterDto>>(parameters);
                    details.InputParameters = parameters.Where(p => !p.IsOutput).ToList();
                    details.OutputParameters = details.InitializeOutputParameters(_mapper);

                }
                return details ?? new TestableObjectDetailsDto();
            }
        }
    }
}
