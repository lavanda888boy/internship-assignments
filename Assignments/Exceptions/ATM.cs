namespace Exceptions
{
    internal class ATM
    {
        private int _availableCash;
        private readonly int _withdrawLimit;
        private readonly int _replenishLimit;
        
        public ATM(int cashAmount)
        {
            _availableCash = cashAmount;
            _withdrawLimit = 10000;
            _replenishLimit = 20000;
        }

        public void ProcessWithdrawal(int cashAmount)
        {
            try
            {
                Console.WriteLine("Withdrawal process started");
                WithDraw(cashAmount);
                Console.WriteLine($"Withdrawal of {cashAmount:C} successful. Available cash in ATM: {_availableCash:C}");
            }
            catch (InvalidTransactionSumException ex) 
            { 
                Console.WriteLine(ex.Message);
            }
            catch (InsufficientCashException ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Withdrawal process ended");
            }
        }

        private void WithDraw(int cashAmount)
        {
            if (cashAmount > _withdrawLimit)
            {
                throw new InvalidTransactionSumException("The customer requested more money than it is permitted for a single transaction", cashAmount);
            }

            if (cashAmount > _availableCash)
            {
                throw new InsufficientCashException("There is not enough cash for the customer's request", cashAmount - _availableCash);
            }

            _availableCash -= cashAmount; 
        }

        public void ProcessReplenishment(Money m)
        {
            try
            {
                Replenish(m);
                Console.WriteLine("Replenishment was succesfully processed");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Money argument null: " + ex.Message);
            }
            catch (InvalidTransactionSumException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Transaction sum: {ex.TransactionSum}");
            }
            finally
            {
                Console.WriteLine("The customer returns into the main menu");
            }
        }

        private void Replenish(Money m)
        {
            if (m is null)
            {
                throw new ArgumentNullException();
            }

            if (m.Value > _replenishLimit)
            {
                throw new InvalidTransactionSumException("The customer tried to replenish the account with more money than it is permitted for a single transaction", m.Value);
            }

            _availableCash += m.Value;
        }
    }
}
