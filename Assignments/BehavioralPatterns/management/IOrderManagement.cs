using BehavioralPatterns.subscriber;

namespace BehavioralPatterns.management
{
    internal interface IOrderManagement
    {
        void PlaceOrder(ISubscriber customer);
    }
}
