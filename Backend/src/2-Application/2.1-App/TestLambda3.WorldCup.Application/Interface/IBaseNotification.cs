using Flunt.Notifications;
using System.Collections.Generic;

namespace TestLambda3.WorldCup.Application.Interface
{
    public interface IBaseNotification
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        bool IsValid { get; }
    }
}
