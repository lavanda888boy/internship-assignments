namespace SOLID.exception
{
    internal class UserDoesNotExistException : Exception
    {
        public int UserId { get; private set; }

        public UserDoesNotExistException(string message) : base(message) { }

        public UserDoesNotExistException(string message, int userId) : this(message)
        {
            UserId = userId;
        }
    }
}
