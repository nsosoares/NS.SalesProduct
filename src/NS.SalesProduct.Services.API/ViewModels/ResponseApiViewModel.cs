using NS.SalesProduct.Business.Notifications;

namespace NS.SalesProduct.Services.API.ViewModels
{
    public class ResponseApiViewModel
    {
        public bool Success { get; set; }
        public List<Notification> Notifications { get; set; }
        public object Data { get; set; }
    }
}
