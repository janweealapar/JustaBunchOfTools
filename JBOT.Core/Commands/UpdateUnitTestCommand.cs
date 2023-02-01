using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Application.Helpers;
using JBOT.Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Commands
{
    public class UpdateUnitTestCommand : IRequest<int>
    {
        public TestableObjectDetailsDto TestableObjectDetailsDto { get; set; }

        public class UpdateUnitTestCommandHandler : IRequestHandler<UpdateUnitTestCommand, int>
        {
            private readonly IApplicationDBContext _context;
            public UpdateUnitTestCommandHandler(IApplicationDBContext context)
            {
                _context= context;
            }
            public async Task<int> Handle(UpdateUnitTestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var unitTest = await _context.UnitTests.FindAsync(request.TestableObjectDetailsDto.Id);
                    if (unitTest != null)
                    {
                        unitTest.TestName = request.TestableObjectDetailsDto.TestName;
                        unitTest.Description= request.TestableObjectDetailsDto.Description;

                        ParameterHelper.SetInputParameterValue(request.TestableObjectDetailsDto.InputParameters, unitTest);
                        ParameterHelper.SetOutputParameterValue(request.TestableObjectDetailsDto.OutputParameters, unitTest);

                        unitTest.StatusId = request.TestableObjectDetailsDto.OutputParameters.Any(p => !(p.IsSuccess ?? false)) ?
                                                (int)StatusEnums.Failed : (int)StatusEnums.Success;

                        _context.Update(unitTest);
                        await _context.SaveChangeAsync();
                        //foreach (var inputParameter in request.TestableObjectDetailsDto.InputParameters)
                        //{
                        //    var parameter = unitTest.Parameters.SingleOrDefault(p => p.ParameterId == inputParameter.Id);
                        //    if (parameter != null)
                        //    {
                        //        parameter.Value = inputParameter.Value;
                        //    }
                        //}

                        //foreach (var outputParameter in request.TestableObjectDetailsDto.OutputParameters)
                        //{
                        //    var assert = unitTest.Assertations.SingleOrDefault(p => p.UnitTestParameter.ParameterId == outputParameter.Id);
                        //    if (assert != null)
                        //    {
                        //        assert.ExpectedValue = outputParameter.Expected;
                        //        assert.OperatorId = (int)outputParameter.Operator;
                        //        assert.ActualValue = outputParameter.Actual;
                        //    }
                        //}

                        return 200;
                    }
                    else
                    {
                        return 404;
                    }
                }
                catch (Exception)
                {
                    return 500;
                }
            }
        }
    }
}
