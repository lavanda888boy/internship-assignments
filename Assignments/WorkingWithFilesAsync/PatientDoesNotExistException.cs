namespace WorkingWithFilesAsync
{
    internal class PatientDoesNotExistException : Exception
    {
        private readonly Patient? _patient;

        public PatientDoesNotExistException(string message) : base(message) { }

        public PatientDoesNotExistException(string message, Patient p) : base(message)
        {
            _patient = p;
        }
    }
}
