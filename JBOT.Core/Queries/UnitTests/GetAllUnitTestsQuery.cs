using AutoMapper;
using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Application.Helpers;
using JBOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Queries.UnitTests
{
    public  class GetAllUnitTestsQuery : IRequest<List<UnitTestDto>>
    {
        public string Server { get; private set; }
        public string Database { get; private set; }
        public GetAllUnitTestsQuery(string server, string database)
        {
            Server = server;
            Database = database;
        }
        public class GetAllUnitTestsQueryHandler : IRequestHandler<GetAllUnitTestsQuery, List<UnitTestDto>>
        {
            private readonly IApplicationDBContext _applicationDBContext;
            private readonly IMapper _mapper;
            public GetAllUnitTestsQueryHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
            {
                _applicationDBContext= applicationDBContext;
                _mapper = mapper;
            }
            public async Task<List<UnitTestDto>> Handle(GetAllUnitTestsQuery request, CancellationToken cancellationToken)
            {
                var unitTests = await _applicationDBContext.UnitTests
                                        .Where(u=> u.Server == request.Server 
                                                    && u.DatabaseName == request.Database).ToListAsync();
                var dto = _mapper.Map<List<UnitTestDto>>(unitTests);

                foreach (var item in dto)
                {
                    var unitTest = unitTests.FirstOrDefault(u => u.Id == item.Id);
                    if (unitTest != null)
                    {
                        item.Parameters = unitTest.GetParameters();
                    }
                }
                return dto;
            }

            

        }
    }
}
