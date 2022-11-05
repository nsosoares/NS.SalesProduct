namespace NS.SalesProduct.Business.Notifications
{
    public sealed class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }

        public Notification(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; private set; }
        public string Code { get; private set; }
    }
}
