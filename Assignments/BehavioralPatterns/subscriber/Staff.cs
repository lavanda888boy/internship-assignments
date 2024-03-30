using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal class Staff : ISubscriber
    {
        public string Name { get; set; }

        public Staff(string name, bool isFree)
        {
            Name = name;
        }

        public void Update(string orderNumber, OrderStatus status)
        {
            Console.WriteLine($"Attention employee {Name}: Order {orderNumber} status is now: {status}");
        }
    }
}
