using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    class UserValidator : IValidator<User>
    {
        private IUserRepo _userRepo;

        public UserValidator(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public List<ValidationResult> Validate(User user)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var vc = new ValidationContext(user);
            Validator.TryValidateObject(user, vc, errors, validateAllProperties: true);

            if (_userRepo.ReadAll().Count(x => x.Email.Equals(user.Email)) > 0)
            {
                errors.Add(new ValidationResult("Email address is already exists!"));
            }

            if (_userRepo.ReadAll().Count(x => x.Username.Equals(user.Username)) > 0)
            {
                errors.Add(new ValidationResult("Username is already exists!"));
            }

            return errors;
        }
    }
}
