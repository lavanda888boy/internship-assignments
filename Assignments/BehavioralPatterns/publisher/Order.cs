using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.publisher
{
    internal class Order : IPublisher
    {
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
        public List<ISubscriber> Staff {  get; set; }

        public Order()
        {
            OrderNumber = Guid.NewGuid().ToString();
            Staff = new List<ISubscriber>();
        }

        public void Notify()
        {
            if (Customer is not null)
            {
                Customer.Update(OrderNumber, Status);
            }

            if (Status == OrderStatus.PLACED || Status == OrderStatus.READY_FOR_SHIPING)
            {
                foreach (var st in Staff)
                {
                    st.Update(OrderNumber, Status);
                }
            }
        }

        public void Attach(ISubscriber subscriber)
        {
            if (subscriber is Customer)
            {
                Customer = subscriber;
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
