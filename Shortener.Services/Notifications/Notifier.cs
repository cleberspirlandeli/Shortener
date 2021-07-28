namespace Shortener.Services.Notifications
{
    public class Notifier
    {
        public Notifier(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
