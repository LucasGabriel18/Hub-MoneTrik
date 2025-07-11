using static Hub.Monetrik.Mediator.Interfaces.IRequest;

namespace Hub.Monetrik.Mediator.Interfaces.Mediator
{
    public interface IMediator
    {
        public interface IMediator
        {
            Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
            Task Send(IRequest request);
            Task Publish<TNotification>(TNotification notification) where TNotification : INotification;
        }
    }
}