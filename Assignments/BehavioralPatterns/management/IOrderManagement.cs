using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;

namespace BehavioralPatterns.management
{
    internal interface IOrderManagement
    {
        List<IPublisher> GetOrders();
        void PlaceOrder(ISubscriber customer);
        void ProcessOrder(string orderNumber);
        void PrepareOrderForShipping(string orderNumber);
    }
}
