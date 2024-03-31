using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal class Customer : ISubscriber
    {
        public string Name { get; set; }
        public NotificationType NotificationType { get; set; }

        public Customer(string name, NotificationType notificationType)
        {
            Name = name;
            NotificationType = notificationType;
        }

        public void Update(string orderNumber, OrderStatus status)
        {
            if (NotificationType == NotificationType.SMS)
            {
                Console.WriteLine("Notification sent via SMS");
            }
            else
            {
                Console.WriteLine("Notification sent via EMAIL");
            }

            Console.WriteLine($"Dear customer {Name}, your order {orderNumber} status is now: {status}");
        }
    }
}
