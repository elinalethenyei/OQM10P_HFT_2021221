using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OQM10P_HFT_2021221.Validation.Exceptions
{
    public class CustomValidationException : ValidationException
    {
        public List<ValidationResult> Errors { get; private set; }

        public CustomValidationException(List<ValidationResult> errors)
        {
            Errors = errors;
        }

        public override string Message => "Invalid entity!";


    }
}
