using JBOT.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Application.Queries.Databases
{
    public class BaseQuery<TResponse> : IRequest<TResponse>
    {
        public string Server { get; set; }
    }

    public abstract class BaseQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest: BaseQuery<TResponse>
    {
        public readonly IValidationContextFactory _validationContextFactory;
        public IValidateDBContext ValidateDBContext { get; private set; }
        public BaseQueryHandler(IValidationContextFactory validationContextFactory)
        {
            _validationContextFactory = validationContextFactory;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            ValidateDBContext = _validationContextFactory.CreateInstanceByServer(request.Server);
            return await DoTask(request, cancellationToken);
        }

        public abstract Task<TResponse> DoTask(TRequest request, CancellationToken cancellationToken);
    }
}
