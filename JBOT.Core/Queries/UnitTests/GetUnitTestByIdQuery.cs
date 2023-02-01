using AutoMapper;
using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Queries.UnitTests
{
    public class GetUnitTestByIdQuery : IRequest<TestableObjectDetailsDto>
    {
        public int Id { get; set; }
        public class GetUnitTestByIdQueryHandler : IRequestHandler<GetUnitTestByIdQuery, TestableObjectDetailsDto>
        {
            private readonly IApplicationDBContext _context;
            private readonly IMapper _mapper;
            public GetUnitTestByIdQueryHandler(IApplicationDBContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<TestableObjectDetailsDto> Handle(GetUnitTestByIdQuery request, CancellationToken cancellationToken)
            {
                var unitTest = await _context.UnitTests.FindAsync(request.Id);
                var dto = new TestableObjectDetailsDto();
                if (unitTest!=null)
                {
                    dto = _mapper.Map<TestableObjectDetailsDto>(unitTest);
                    dto.InputParameters = _mapper.Map<List<ParameterDto>>(unitTest.Parameters.Where(p => !p.IsOutput && p.ParameterId>0));
                    dto.OutputParameters = _mapper.Map<List<OutputParameterDto>>(unitTest.Assertations);

                    dto.Status = null;
                }
                else
                {
                    dto.Status = Domain.Entities.Enums.StatusEnums.Failed;
                    dto.ErrorMessage = "Object not exists or was removed.";
                }
                return dto;
            }
        }
    }
}
