namespace Hospital.Application.Exceptions
{
    public class UserLoginException : Exception
    {
        public UserLoginException(string message) : base(message) { }
    }
}
