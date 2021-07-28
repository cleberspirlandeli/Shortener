using System.Collections.Generic;

namespace Shortener.Services.Notifications
{
    public interface INotification
    {
        bool HasNotification();
        List<Notifier> GetNotifications();
        void Handle(Notifier notifier);
    }
}
