using static Hub.Monetrik.Mediator.Interfaces.INotificationHandler;

namespace Hub.Monetrik.Domain.Notifications
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        private readonly List<Notification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public Task Handle(Notification notification)
        {
            // Evita mensagens duplicadas
            if (!_notifications.Any(n => n.Message == notification.Message))
            {
                _notifications.Add(notification);
            }
            return Task.CompletedTask;
        }

        public List<Notification> GetNotifications()
            => _notifications;

        public bool HasNotifications()
            => _notifications.Any();

        public void ClearNotifications()
            => _notifications.Clear();

        public void GetTypeNotification()
            => _notifications.Select(x => x.Type);
    }
    
}