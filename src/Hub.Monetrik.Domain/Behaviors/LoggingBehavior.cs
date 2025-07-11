using Hub.Monetrik.Mediator.Interfaces;
using Microsoft.Extensions.Logging;
using static Hub.Monetrik.Mediator.Interfaces.IPipelineBehavior;
namespace Hub.Monetrik.Domain.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            
            var response = await next();
            
            _logger.LogInformation($"Handled {typeof(TRequest).Name}");
            
            return response;
        }
    }
}