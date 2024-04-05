namespace Hospital.Application.Exceptions
{
    public class NoEntityFoundException : Exception
    {
        public NoEntityFoundException(string message) : base(message) { }
    }
}
