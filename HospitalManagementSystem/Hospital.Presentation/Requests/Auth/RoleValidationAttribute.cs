using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Requests.Auth
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valueString = value as string;

            if (valueString != null)
            {
                return valueString switch
                {
                    "Admin" => true,
                    "PatientUser" => true,
                    "DoctorUser" => true,
                    _ => false,
                };
            }
            ErrorMessage = "Invalid user role value";
            return false;
        }
    }
}
