namespace SOLID
{
    internal class SMSNotification : Notification
    {
        public SMSNotification(NotificationMessage message) : base(message) { }

        public override void Send()
        {
            Console.WriteLine($"{Message.Sender.Name} sends to {Message.Recipient.Name} SMS: {Message.Content}");
        }
    }
}
