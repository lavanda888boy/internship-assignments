using BehavioralPatterns.publisher;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal class Customer : ISubscriber
    {
        private List<Order> _attachedOrders = new List<Order>();

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

        public bool TryAttachToOrder(Order order)
        {
            if (_attachedOrders.Contains(order))
            {
                Console.WriteLine("Customer already attached to order");
                return false;
            }
            
            _attachedOrders.Add(order);
            return true;
        }

        public void DetachFromOrder(Order order)
        {
            _attachedOrders.Remove(order);
        }
    }
}
