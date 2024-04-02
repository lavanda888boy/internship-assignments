using BehavioralPatterns.exception;
using BehavioralPatterns.management;
using BehavioralPatterns.subscriber;
using BehavioralPatterns.utility;

namespace BehavioralPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer("Seva", NotificationType.EMAIL);
            OrderManager ordm = new OrderManager();
            try
            {
                ordm.PlaceOrder(customer);

                string orderNumber = ordm.GetOrders().Last().OrderNumber;
                ordm.ProcessOrder(orderNumber);
                ordm.PrepareOrderForShipping(orderNumber);
            }
            catch (NoStaffAvailableException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
