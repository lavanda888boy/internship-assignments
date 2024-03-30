using SOLID.entity;

namespace SOLID.service
{
    internal class SMSNotificationService : INotificationService
    {
        public void SendNotification(User sender, User recipient, Notification n)
        {
            Console.WriteLine($"{sender.Name} {sender.PhoneNumber} sent notification to {recipient.Name} {recipient.PhoneNumber}");
            Console.WriteLine(n.ToString());
        }
    }
}
