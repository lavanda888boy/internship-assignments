using SOLID.entity;

namespace SOLID.service
{
    internal class PushNotificationService : INotificationService
    {
        public void SendNotification(User sender, User recipient, Notification n)
        {
            Console.WriteLine($"{sender.Name} {sender.AppToken} sent notification to {recipient.Name} {recipient.AppToken}");
            Console.WriteLine(n.ToString());
        }
    }
}
