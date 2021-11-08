using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    public class ModelValidator
    {

        private IUserRepo _userRepo;

        public ModelValidator(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public bool Validate(object instance)
        {
            List<ValidationResult> errors = new();
            if (instance is User user)
            {
                errors = new UserValidator(_userRepo).Validate(user);
            }

            if (errors.Count > 0)
            {
                throw new CustomValidationException(errors);
            }
            return true;
        }
    }
}
