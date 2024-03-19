namespace Exceptions
{
    internal class ATM
    {
        private int _availableCash;
        private readonly int _withdrawLimit;

        public ATM(int cashAmount)
        {
            _availableCash = cashAmount;
            _withdrawLimit = 10000;
        }

        public void WithDraw(int cashAmount)
        {
            
        }
    }
}
