namespace Hospital.Application.Exceptions
{
    public class DoctorAssignedPatientsLimitExceeded : Exception
    {
        public DoctorAssignedPatientsLimitExceeded(string message) : base(message) { }
    }
}
