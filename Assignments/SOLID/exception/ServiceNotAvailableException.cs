namespace SOLID.exception
{
    internal class ServiceNotAvailableException : Exception
    {
        public ServiceNotAvailableException(string message) : base(message) { }
    }
}
