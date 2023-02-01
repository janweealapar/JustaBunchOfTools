using AutoMapper;
using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Commands
{
    public class CreateUnitTestCommand : IRequest<int>
    {
        public TestableObjectDetailsDto TestableObjectDetailsDto { get; set; }
        public class CreateUnitTestCommandHandler : IRequestHandler<CreateUnitTestCommand, int>
        {
            private readonly IApplicationDBContext _applicationDBContext;
            private readonly IMapper _mapper;
            public CreateUnitTestCommandHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
            {
                _applicationDBContext = applicationDBContext;
                _mapper = mapper; 
            }
            public async Task<int> Handle(CreateUnitTestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var unitTest = _mapper.Map<UnitTest>(request.TestableObjectDetailsDto);
                    unitTest.Parameters.AddRange(
                        _mapper.Map<IEnumerable<UnitTestParameter>>(request.TestableObjectDetailsDto.InputParameters));

                    unitTest.Parameters.AddRange(
                        _mapper.Map<IEnumerable<UnitTestParameter>>(request.TestableObjectDetailsDto.OutputParameters));

                    var assert = request.TestableObjectDetailsDto.OutputParameters
                                        .Join(unitTest.Parameters, 
                                              dto => dto.Id, 
                                              p=> p.ParameterId,
                                              (dto, p) => new UnitTestAssertation() 
                                              { 
                                                    UnitTestParameter = p,
                                                    ExpectedValue = dto.Expected,
                                                    OperatorId = (int)dto.Operator,
                                                    ActualValue = dto.Actual,
                                                    StatusId = (dto.IsSuccess ?? false) ? (int)StatusEnums.Success : (int)StatusEnums.Failed
                                              }).ToList();
                    unitTest.Assertations.AddRange(assert);

                    request.TestableObjectDetailsDto.Status = StatusEnums.Success;
                    request.TestableObjectDetailsDto.ErrorTitle = "Unit Test Saved.";

                    _applicationDBContext.Add(unitTest);
                    return await _applicationDBContext.SaveChangeAsync();
                }
                catch (Exception ex)
                {
                    request.TestableObjectDetailsDto.Status = StatusEnums.Failed;
                    request.TestableObjectDetailsDto.ErrorTitle = "An error occured.";
                    request.TestableObjectDetailsDto.ErrorMessage = ex.Message;
                    return 99;
                }
                
            }
        }
    }
}
