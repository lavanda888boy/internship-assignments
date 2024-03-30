using BehavioralPatterns.utility;

namespace BehavioralPatterns.subscriber
{
    internal interface ISubscriber
    {
        void Update(string orderNumber, OrderStatus status);
    }
}
