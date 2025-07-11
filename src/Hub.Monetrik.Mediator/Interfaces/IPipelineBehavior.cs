namespace Hub.Monetrik.Mediator.Interfaces
{
    public interface IPipelineBehavior
    {
        public interface IPipelineBehavior<TRequest, TResponse>
        {
            Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next);
        }
    }
}