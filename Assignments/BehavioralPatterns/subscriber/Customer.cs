using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal class Customer : ISubscriber
    {
        public string Name { get; set; }

        public Customer(string name)
        {
            Name = name;
        }

        public void Update(string orderNumber, OrderStatus status)
        {
            Console.WriteLine($"Dear customer {Name}, your order {orderNumber} status is now: {status}");
        }
    }
}
