namespace Exceptions
{
    internal class Money
    {
        private int _value;
        public int Value 
        { 
            get => _value; 
            set => _value = value; 
        }

        private string _currency;
        public string Currency 
        { 
            get => _currency;
            set => _currency = value; 
        }

        public Money(int value, string currency)
        {
            Value = value;
            Currency = currency;
        }
    }
}
