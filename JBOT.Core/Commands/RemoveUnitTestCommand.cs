using JBOT.Application.Common.Interfaces;
using JBOT.Domain.Entities;
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
    public class RemoveUnitTestCommand : IRequest<int>
    {
        public int UnitTestId { get; set; }
        public class RemoveUnitTestCommandHandler : IRequestHandler<RemoveUnitTestCommand, int>
        {
            private IApplicationDBContext _applicationDBContext;
            public RemoveUnitTestCommandHandler(IApplicationDBContext applicationDBContext)
            {
                _applicationDBContext = applicationDBContext;
            }
            public async Task<int> Handle(RemoveUnitTestCommand request, CancellationToken cancellationToken)
            {
                int result = 0;
                try
                {
                    var unitTest = await _applicationDBContext.UnitTests
                                            .Include(u=> u.Parameters)
                                            .Include(u=> u.Assertations)
                                            .FirstOrDefaultAsync(u=> u.Id == request.UnitTestId);
                    unitTest.Parameters.Clear();
                    unitTest.Assertations.Clear();
                    _applicationDBContext.Delete(unitTest);
                    result = await _applicationDBContext.SaveChangeAsync();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                return result;
            }
        }
    }
}
