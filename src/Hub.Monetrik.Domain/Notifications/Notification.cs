using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Mediator.Interfaces;

namespace Hub.Monetrik.Domain.Notifications
{
    public class Notification : INotification
    {
        public string Message { get; }
        public ENotificationType Type { get; }
        public DateTime Timestamp { get; }

        public Notification(string message, ENotificationType type)
        {
            Message = message;
            Type = type;
            Timestamp = DateTime.Now;
        }
    }
}