using NS.SalesProduct.Business.Interfaces;

namespace NS.SalesProduct.Business.Notifications
{
    public class NotificationHandler : INotificationHandler
    {
        private List<Notification> _notifications;
        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> GetNotifications()
            => _notifications;

        public void Handle(string message)
            => _notifications.Add(new Notification(message));


        public void Handle(string message, string code)
            => _notifications.Add(new Notification(message, code));
      

        public bool HasNotifications()
            => _notifications.Any();
        

        public void Dispose()
          =>  _notifications = new List<Notification>();
    }
}
