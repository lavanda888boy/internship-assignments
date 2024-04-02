using BehavioralPatterns.management;
using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.publisher
{
    internal class Order : IPublisher
    {
        private readonly OrderNotificationService _notificationService;
        public string OrderNumber { get; }

        private OrderStatus _status;
        public OrderStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                Notify();
            }
        }
        public ISubscriber? Customer { get; set; }
        public List<ISubscriber> Staff { get; set; }

        public Order(OrderNotificationService notificationService)
        {
            OrderNumber = Guid.NewGuid().ToString();
            Staff = new List<ISubscriber>();
            _notificationService = notificationService;
        }

        public void Notify()
        {
            _notificationService.NotifyOrderSubscribers(this);
        }

        public void Attach(ISubscriber subscriber)
        {
            if (subscriber is Customer)
            {
                Customer = subscriber;
                subscriber.TryAttachToOrder(this);
                Console.WriteLine("Customer was attached to the order");
            }
            else
            {
                Staff.Add(subscriber);
                Console.WriteLine("Staff member was attached to the order");
            }
        }

        public void Detach(ISubscriber subscriber)
        {
            if (subscriber is Customer)
            {
                Customer = null;
                subscriber.DetachFromOrder(this);
                Console.WriteLine("Customer was detached from order");
            }
            else
            {
                Staff.Remove(subscriber);
                Console.WriteLine("Staff member was detached from order");
            }
         }
    }
}
