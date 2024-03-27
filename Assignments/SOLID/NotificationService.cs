namespace SOLID
{
    internal class NotificationService
    {
        public void SendNotification(Notification notification)
        {
            Console.WriteLine("Sending notification via notification service..");
            notification.Send();
        }
    }
}
