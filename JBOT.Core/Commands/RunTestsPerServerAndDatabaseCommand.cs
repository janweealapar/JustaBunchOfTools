using AutoMapper;
using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Application.Helpers;
using JBOT.Application.Queries.Databases;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Commands
{
    public class RunTestsPerServerAndDatabaseCommand : BaseQuery<List<UnitTestDto>>
    {
        public string DatabaseName { get; private set; }
        public RunTestsPerServerAndDatabaseCommand(string server, string database)
        {
            Server= server;
            DatabaseName = database;
        }

        public class RunTestsPerServerAndDatabaseCommandHandler : BaseQueryHandler<RunTestsPerServerAndDatabaseCommand, List<UnitTestDto>>
        {
            private readonly IApplicationDBContext _applicationDBContext;
            private readonly IMapper _mapper;
            public RunTestsPerServerAndDatabaseCommandHandler(IValidationContextFactory factory, 
                IApplicationDBContext applicationDBContext, IMapper mapper) : base(factory)
            {
                _applicationDBContext = applicationDBContext;
                _mapper = mapper;
            }
            public override async Task<List<UnitTestDto>> DoTask(RunTestsPerServerAndDatabaseCommand request, CancellationToken cancellationToken)
            {

                var unitTestList = new List<UnitTestDto>();
                var taskList = new List<Task<TestableObjectDetailsDto>>();
                var unitTests = await _applicationDBContext.UnitTests
                                        .Include(i => i.Parameters)
                                        .Include(i => i.Assertations)
                                        .Where(u => u.Server == request.Server
                                                    && u.DatabaseName == request.DatabaseName).ToListAsync();

                if (unitTests != null)
                {
                    var dtoList = new List<TestableObjectDetailsDto>();
                    foreach (var item in unitTests)
                    {
                        var dto = _mapper.Map<TestableObjectDetailsDto>(item);
                        dto.InputParameters = _mapper.Map<List<ParameterDto>>(item.Parameters.Where(p => !p.IsOutput && p.ParameterId > 0));
                        dto.OutputParameters = _mapper.Map<List<OutputParameterDto>>(item.Assertations);
                        dto.Status = null;
                        dtoList.Add(dto);
                    }
                    await ValidateDBContext.RunUnitTest(dtoList);

                    foreach (var dto in dtoList)
                    {
                        var unitTest = unitTests.FirstOrDefault(u => u.Id == dto.Id);
                        ParameterHelper.SetInputParameterValue(dto.InputParameters, unitTest);
                        ParameterHelper.SetOutputParameterValue(dto.OutputParameters, unitTest);

                        unitTest.StatusId = dto.OutputParameters.Any(p => !(p.IsSuccess ?? false)) ?
                                                (int)StatusEnums.Failed : (int)StatusEnums.Success;

                        _applicationDBContext.Update(unitTest);
                    }

                    await _applicationDBContext.SaveChangeAsync();

                    unitTestList = _mapper.Map<List<UnitTestDto>>(unitTests);

                    foreach (var item in unitTestList)
                    {
                        var unitTest = unitTests.FirstOrDefault(u => u.Id == item.Id);
                        if (unitTest != null)
                        {
                            item.Parameters = unitTest.GetParameters();
                        }
                    }
                }
                return unitTestList;
            }
        }
    }
}
