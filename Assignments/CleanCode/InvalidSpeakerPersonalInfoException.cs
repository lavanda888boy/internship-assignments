namespace CleanCode
{
    internal class InvalidSpeakerPersonalInfoException : Exception
    {
        public InvalidSpeakerPersonalInfoException(string message)
                : base(message) { }
    }
}
