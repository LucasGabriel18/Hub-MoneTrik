namespace Hub.Monetrik.Mediator.Interfaces
{
    public interface INotificationHandler
    {
        public interface INotificationHandler<TNotification> where TNotification : INotification
        {
            Task Handle(TNotification notification);
        }
    }
}