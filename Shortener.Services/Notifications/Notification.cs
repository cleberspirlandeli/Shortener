using System.Collections.Generic;
using System.Linq;

namespace Shortener.Services.Notifications
{
    public class Notification : INotification
    {
        private List<Notifier> _notifiers;

        public Notification()
        {
            _notifiers = new List<Notifier>();
        }

        public void Handle(Notifier notifier) => _notifiers.Add(notifier);

        public List<Notifier> GetNotifications() => _notifiers;

        public bool HasNotification() => _notifiers.Any();
    }
}
