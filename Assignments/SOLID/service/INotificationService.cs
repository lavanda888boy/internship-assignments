using SOLID.entity;

namespace SOLID.service
{
    internal interface INotificationService
    {
        void SendNotification(User sender, User recipient, Notification n);
    }
}
