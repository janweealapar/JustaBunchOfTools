using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using JBOT.Application.Queries.Databases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Commands
{
    public class ExecuteUnitTestCommand : BaseQuery<TestableObjectDetailsDto>
    {
        public ExecuteUnitTestCommand(string server)
        {
            Server= server;
        }
        public TestableObjectDetailsDto UnitTest { get; set; }

        public class ExecuteUnitTestCommandHandler : BaseQueryHandler<ExecuteUnitTestCommand, TestableObjectDetailsDto>
        {
            public ExecuteUnitTestCommandHandler(IValidationContextFactory factory):base(factory) { }

            public override async Task<TestableObjectDetailsDto> DoTask(ExecuteUnitTestCommand request, CancellationToken cancellationToken)
            {
                await ValidateDBContext.RunUnitTest(request.UnitTest);
                return request.UnitTest;
            }
        }
    }
}
