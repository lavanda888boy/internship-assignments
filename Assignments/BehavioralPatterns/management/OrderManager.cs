using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

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

        public List<IPublisher> GetOrders()
        {
            return _orders;
        }

        public void PlaceOrder(ISubscriber customer)
        {
            List<ISubscriber> orderStaff = _staff.Take(_staffCount).ToList();
            IPublisher order = new Order();

            order.Attach(customer);
            orderStaff.ForEach(s => order.Attach(s));
            ((Order)order).Status = OrderStatus.PLACED;

            _orders.Add(order);
        }

        public void ProcessOrder(string orderNumber)
        {
            var order = _orders.Cast<Order>().First(o => o.OrderNumber == orderNumber);
            order.Status = OrderStatus.PROCESSING;
        }

        public void PrepareOrderForShipping(string orderNumber)
        {
            var order = _orders.Cast<Order>().First(o => o.OrderNumber == orderNumber);
            order.Status = OrderStatus.READY_FOR_SHIPING;
            
            var staffListCopy = order.Staff.ToList();
            staffListCopy.ForEach(s => order.Detach(s));
        }
    }
}
