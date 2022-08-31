using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviors
{
    public class UnhandlerExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : Exception, IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandlerExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            // Solo analizar lós métodos Handlers de los Querys y Commands
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex,
                    $"Application Request: Sucedió una excepción para el request {requestName} {request}");
                throw;
            }
        }
    }
}
