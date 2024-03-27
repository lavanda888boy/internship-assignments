namespace SOLID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User sender = new("Bob", "bob@mail.com");
            User recipient = new("Alice", "alice@mail.com");
            string content = "Hello, my friend";

            Notification n = new EmailNotification(new NotificationMessage(sender, recipient, content));
            NotificationService service = new NotificationService();
            service.SendNotification(n);

            n.Message = new NotificationMessage(recipient, sender, "Oh, hi there");
            service.SendNotification(n);

            Notification n1 = new SMSNotification(new NotificationMessage(sender, recipient, content));
            service.SendNotification(n1);
        }
    }
}
