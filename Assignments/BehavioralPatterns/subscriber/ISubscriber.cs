using BehavioralPatterns.publisher;
using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal interface ISubscriber
    {
        void Update(string orderNumber, OrderStatus status);
        bool TryAttachToOrder(Order order);
        void DetachFromOrder(Order order);
    }
}
