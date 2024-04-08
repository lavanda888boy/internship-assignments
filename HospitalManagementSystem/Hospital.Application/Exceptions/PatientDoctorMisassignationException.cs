namespace Hospital.Application.Exceptions
{
    public class PatientDoctorMisassignationException : Exception
    {
        public PatientDoctorMisassignationException(string message) : base(message) { } 
    }
}
