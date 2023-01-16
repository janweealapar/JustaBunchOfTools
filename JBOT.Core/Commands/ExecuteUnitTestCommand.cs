using JBOT.Application.Common.Interfaces;
using JBOT.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Commands
{
    public class ExecuteUnitTestCommand : IRequest<TestableObjectDetailsDto>
    {
        public TestableObjectDetailsDto UnitTest { get; set; }

        public class ExecuteUnitTestCommandHandler : IRequestHandler<ExecuteUnitTestCommand, TestableObjectDetailsDto>
        {
            private readonly IValidateDBContext _dbContext;
            public ExecuteUnitTestCommandHandler(IValidateDBContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<TestableObjectDetailsDto> Handle(ExecuteUnitTestCommand request, CancellationToken cancellationToken)
            {
                await _dbContext.RunUnitTest(request.UnitTest);
                return request.UnitTest;
            }
        }
    }
}
