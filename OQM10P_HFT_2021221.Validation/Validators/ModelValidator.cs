using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    public class ModelValidator
    {

        private IUserRepo _userRepo;
        private IIssueRepo _issueRepo;
        private IProjectRepo _projectRepo;
        private IValidator<Issue> _issueValidator;
        private IValidator<Project> _projectValidator;
        private IValidator<User> _userValidator;

        public ModelValidator(IUserRepo userRepo, IIssueRepo issueRepo, IProjectRepo projectRepo, IValidator<Issue> issueValidator, IValidator<Project> projectValidator, IValidator<User> userValidator)
        {
            _userRepo = userRepo;
            _issueRepo = issueRepo;
            _projectRepo = projectRepo;
            _issueValidator = issueValidator;
            _projectValidator = projectValidator;
            _userValidator = userValidator;
        }

        public bool Validate(object instance)
        {
            List<ValidationResult> errors = new();
            if (instance is User user)
            {
                errors = _userValidator.Validate(user);
            }
            else if (instance is Issue issue)
            {
                errors = _issueValidator.Validate(issue);
            }
            else if (instance is Project project)
            {
                errors = _projectValidator.Validate(project);
            }

            if (errors.Count > 0)
            {
                throw new CustomValidationException(errors);
            }
            return true;
        }
    }
}
