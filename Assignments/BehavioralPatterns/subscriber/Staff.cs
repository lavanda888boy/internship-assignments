using BehavioralPatterns.exception;
using BehavioralPatterns.publisher;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal class Staff : ISubscriber
    {
        private int _maximumOrderNumberToProcess = 2;
        private List<Order> _attachedOrders = new List<Order>();

        public string Name { get; set; }

        public Staff(string name)
        {
            Name = name;
        }

        public void Update(string orderNumber, OrderStatus status)
        {
            Console.WriteLine($"Attention employee {Name}: Order {orderNumber} status is now: {status}");
        }

        public bool TryAttachToOrder(Order order)
        {
            if (_attachedOrders.Count == _maximumOrderNumberToProcess)
            {
                Console.WriteLine("Staff member is overloaded with orders");
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
