using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Validation.Exceptions
{
    public class CustomValidationException : ValidationException
    {
        private List<ValidationResult> _errors;

        public CustomValidationException(List<ValidationResult> errors)
        {
            _errors = errors;
        }

        public override string Message => string.Join("\r\n", _errors);


    }
}
