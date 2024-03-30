using SOLID.entity;

namespace SOLID.service
{
    internal class EmailNotificationService : INotificationService
    {
        public void SendNotification(User sender, User recipient, Notification n)
        {
            Console.WriteLine($"{sender.Name} {sender.Email} sent notification to {recipient.Name} {recipient.Email}");
            Console.WriteLine(n.ToString());
        }
    }
}
