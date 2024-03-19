namespace Exceptions
{
    internal class InvalidWithdrawalSum : Exception
    {
        private int _withdrawalSum;

        public InvalidWithdrawalSum() { }

        public InvalidWithdrawalSum(string message) : base(message) { }

        public InvalidWithdrawalSum(string message, Exception inner) : base(message, inner) { }

        public InvalidWithdrawalSum(string message, int withdrawalSum) : this(message)
        {
            _withdrawalSum = withdrawalSum;
        }
    }
}
