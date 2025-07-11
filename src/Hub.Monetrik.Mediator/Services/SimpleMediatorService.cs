using Hub.Monetrik.Mediator.Interfaces;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Microsoft.Extensions.DependencyInjection;
using static Hub.Monetrik.Mediator.Interfaces.INotificationHandler;
using static Hub.Monetrik.Mediator.Interfaces.IPipelineBehavior;
using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Mediator.Services
{
    public class SimpleMediatorService : IMediator
    {
        private readonly IServiceProvider _provider;

        public SimpleMediatorService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _provider.GetService(handlerType) ?? throw new InvalidOperationException($"Handler for {request.GetType().Name} not found.");
            var pipelineType = typeof(IPipelineBehavior<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var pipelines = _provider.GetServices(pipelineType).Cast<dynamic>().Reverse().ToList();

            Func<Task<TResponse>> invokeHandler = () => (Task<TResponse>)handlerType.GetMethod("Handle")
                .Invoke(handler, new object[] { request });

            foreach (var behavior in pipelines)
            {
                var next = invokeHandler;
                invokeHandler = () => behavior.Handle((dynamic)request, next);
            }

            return await invokeHandler();
        }
        public async Task Send(IRequest request)
        {
            var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            var handler = _provider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Handler for {request.GetType().Name} not found.");

            await (Task)handlerType.GetMethod("Handle").Invoke(handler, new object[] { request });
        }
        public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(typeof(TNotification));
            var handlers = _provider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await (Task)handlerType.GetMethod("Handle").Invoke(handler, new object[] { notification });
            }
        }
    }
}