using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.management
{
    internal class OrderNotificationService
    {
        public void NotifyOrderSubscribers(Order order)
        {
            if (order.Customer is not null)
            {
                order.Customer.Update(order.OrderNumber, order.Status);
            }

            if (order.Status == OrderStatus.PLACED || order.Status == OrderStatus.READY_FOR_SHIPING)
            {
                order.Staff.ForEach(st => st.Update(order.OrderNumber, order.Status));
            }
        }
    }
}
