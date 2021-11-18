using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    public class UserValidator : IValidator<User>
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

            if (user.Id != null)
            {
                User savedUser = _userRepo.Read((int)user.Id);
                if (savedUser == null)
                {
                    errors.Add(new ValidationResult($"User with the given id does not exists! Id: {user.Id}"));
                }
            }

            var all = _userRepo.ReadAll();
            var email = _userRepo.ReadAll().Where(x => x.Email.Equals(user.Email));
            var emailandid = _userRepo.ReadAll().Where(x => x.Email.Equals(user.Email) && !x.Id.Equals(user.Id));
            if (user.Email != null && _userRepo.ReadAll().Where(x => x.Email.Equals(user.Email) && !x.Id.Equals(user.Id)).Count() > 0)
            {
                errors.Add(new ValidationResult($"Email address already exists! Email: {user.Email}"));
            }

            if (user.Username != null && _userRepo.ReadAll().Count(x => x.Username.Equals(user.Username)) > 0)
            { 
                errors.Add(new ValidationResult($"Username already exists! Username: {user.Username}"));
            }

            return errors;
        }
    }
}
