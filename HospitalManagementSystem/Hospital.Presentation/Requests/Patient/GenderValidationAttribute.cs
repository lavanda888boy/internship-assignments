using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Dto.Patient
{
    public class GenderValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valueString = value as string;

            if (valueString == null)
            {
                ErrorMessage = "Invalid gender value";
                return false;
            }

            return valueString switch
            {
                "M" => true,
                "F" => true,
                "Other" => true,
                _ => false,
            };
        }
    }
}
