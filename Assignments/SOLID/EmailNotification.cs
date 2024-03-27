namespace SOLID
{
    internal class EmailNotification : Notification
    {
        public EmailNotification(NotificationMessage message) : base(message) { }

        public override void Send()
        {
            Console.WriteLine($"{Message.Sender.Email} sends to {Message.Recipient.Email} an email: {Message.Content}");
        }
    }
}
