namespace Hospital.Application.Exceptions
{
    public class DoctorPatientAssignationException : Exception
    {
        public DoctorPatientAssignationException(string message) : base(message) { }
    }
}
