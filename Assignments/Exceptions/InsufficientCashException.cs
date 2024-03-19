namespace Exceptions
{
    internal class InsufficientCashException : Exception
    {
        private int _cashDifference;

        public InsufficientCashException() { }

        public InsufficientCashException(string message) : base(message) { }

        public InsufficientCashException(string message, Exception inner) : base(message, inner) { }

        public InsufficientCashException(string message, int cashDifference) : this(message)
        {
            _cashDifference = cashDifference;
        }
    }
}
