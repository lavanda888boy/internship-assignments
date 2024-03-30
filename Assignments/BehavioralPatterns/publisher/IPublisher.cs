using BehavioralPatterns.subscriber;

namespace BehavioralPatterns.publisher
{
    internal interface IPublisher
    {
        void Attach(ISubscriber subscriber);
        void Detach(ISubscriber subscriber);
        void Notify();
    }
}
