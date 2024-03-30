using BehavioralPatterns.management;
using BehavioralPatterns.subscriber;

namespace BehavioralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISubscriber customer = new Customer("Seva");
            IOrderManagement ordm = new OrderManager();
            ordm.PlaceOrder(customer);
        }
    }
}
