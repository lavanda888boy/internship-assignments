namespace Exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM(100000);
            Money m = new Money(30000, "usd");
            atm.Replenish(m);
        }
    }
}
