namespace SOLID
{
    internal class PushNotification : Notification
    {
        public PushNotification(NotificationMessage message) : base(message) { }

        public override void Send()
        {
            Console.WriteLine($"{Message.Sender.Name} sends to {Message.Recipient.Name} push notification: {Message.Content}");
        }
    }
}
