

using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;

namespace Hub.Monetrik.Mediator.Interfaces.Mediator
{
    public interface IRequestHandler
    {
        public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
        {
            Task<TResponse> Handle(TRequest request);
        }

        public interface IRequestHandler<TRequest> where TRequest : IRequest
        {
            Task Handle(TRequest request);
        }
    }
}