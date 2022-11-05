using NS.SalesProduct.Business.Notifications;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface INotificationHandler : IDisposable
    {
        void Handle(string message);
        void Handle(string message, string code);
        bool HasNotifications();
        IReadOnlyCollection<Notification> GetNotifications(); 
    }
}
