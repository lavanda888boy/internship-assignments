namespace SOLID
{
    internal class NotificationService
    {
        public static void SendNotification(Notification notification)
        {
            Console.WriteLine("Sending notification via notification service..");
            notification.Send();
        }
    }
}
