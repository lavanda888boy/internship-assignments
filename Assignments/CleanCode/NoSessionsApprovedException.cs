namespace CleanCode
{
    internal class NoSessionsApprovedException : Exception
    {
        public NoSessionsApprovedException(string message)
                : base(message) { }
    }
}
