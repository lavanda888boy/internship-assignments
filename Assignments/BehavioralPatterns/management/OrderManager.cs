using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;

namespace BehavioralPatterns.management
{
    internal class OrderManager : IOrderManagement
    {
        private List<IPublisher> _orders;
        private List<ISubscriber> _staff = new()
        {
            new Staff("Denis", true),
            new Staff("Mike", true),
            new Staff("Steve", false),
            new Staff("Harvey", true)
        };
        private readonly int _staffCount = 2;

        public OrderManager()
        {
            _orders = new List<IPublisher>();
        }

        public void PlaceOrder(ISubscriber customer)
        {
            List<ISubscriber> orderStaff = _staff.Take(_staffCount).ToList();
            IPublisher order = new Order(customer, orderStaff);
            _orders.Add(order);
        }
    }
}
