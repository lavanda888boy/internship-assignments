using BehavioralPatterns.management;
using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

namespace BehavioralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISubscriber customer = new Customer("Seva", NotificationType.EMAIL);
            IOrderManagement ordm = new OrderManager();
            ordm.PlaceOrder(customer);

            string orderNumber = ((Order) ordm.GetOrders().Last()).OrderNumber;
            ordm.ProcessOrder(orderNumber);
            ordm.PrepareOrderForShipping(orderNumber);

            ordm.PlaceOrder(customer);
        }
    }
}
