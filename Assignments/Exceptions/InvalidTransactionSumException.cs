namespace Exceptions
{
    internal class InvalidTransactionSumException : Exception
    {
        private int _transactionSum;
        public int TransactionSum 
        { 
            get => _transactionSum; 
        }

        public InvalidTransactionSumException() { }

        public InvalidTransactionSumException(string message) : base(message) { }

        public InvalidTransactionSumException(string message, Exception inner) : base(message, inner) { }

        public InvalidTransactionSumException(string message, int transactionSum) : this(message)
        {
            _transactionSum = transactionSum;
        }
    }
}
