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

        public Order(ISubscriber customer, List<ISubscriber> staff)
        {
            OrderNumber = Guid.NewGuid().ToString();
            Customer = customer;
            Staff = staff;
            Status = OrderStatus.PLACED;
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
            Staff.Add(subscriber);
        }

        public void Detach(ISubscriber subscriber)
        {
            if (subscriber is Customer)
            {
                Customer = null;
            }
            else
            {
                Staff.Remove(subscriber as ISubscriber);
            }
         }
    }
}
