using BehavioralPatterns.exception;
using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.management
{
    internal class OrderManager : IOrderManagement
    {
        private List<Order> _orders = new List<Order>();
        private List<Staff> _staff = new()
        {
            new Staff("Denis"),
            new Staff("Mike"),
            new Staff("Steve"),
            new Staff("Harvey")
        };
        private const int _staffPerOrderLimit = 2;

        public OrderManager() { }

        public List<Order> GetOrders()
        {
            return _orders;
        }

        public void PlaceOrder(Customer customer)
        {
            Order order = new Order(new OrderNotificationService());
            var orderStaff = new List<Staff>();
            int staffCount = 0;
            orderStaff = _staff.Where(st => st.TryAttachToOrder(order) && ++staffCount <= _staffPerOrderLimit)
                               .ToList();

            if (orderStaff.Count < _staffPerOrderLimit)
            {
                throw new NoStaffAvailableException("There are no employees to process the order");
            }

            orderStaff.ForEach(order.Attach);
            order.Attach(customer);
            
            order.Status = OrderStatus.PLACED;
            _orders.Add(order);
        }

        public void ProcessOrder(string orderNumber)
        {
            var order = _orders.First(o => o.OrderNumber == orderNumber);
            order.Status = OrderStatus.PROCESSING;
        }

        public void PrepareOrderForShipping(string orderNumber)
        {
            var order = _orders.First(o => o.OrderNumber == orderNumber);
            order.Status = OrderStatus.READY_FOR_SHIPING;
            
            var staffListCopy = order.Staff.ToList();
            staffListCopy.ForEach(order.Detach);
            staffListCopy.ForEach(s => s.DetachFromOrder(order));
        }
    }
}
