namespace Hospital.Infrastructure.Exceptions
{
    public class EntityNotFoundByIdException : Exception
    {
        public EntityNotFoundByIdException(string message) : base(message) { }
    }
}
