namespace SOLID.infrastructure
{
    internal interface INotificationManager
    {
        void Notify(int senderId, int recipientId, string message, NotificationType notificationType);
    }
}
