using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    public class ModelValidator
    {

        private IUserRepo _userRepo;
        private IIssueRepo _issueRepo;
        private IProjectRepo _projectRepo;

        public ModelValidator(IUserRepo userRepo, IIssueRepo issueRepo, IProjectRepo projectRepo)
        {
            _userRepo = userRepo;
            _issueRepo = issueRepo;
            _projectRepo = projectRepo;
        }

        public bool Validate(object instance)
        {
            List<ValidationResult> errors = new();
            if (instance is User user)
            {
                errors = new UserValidator(_userRepo).Validate(user);
            }
            else if (instance is Issue issue)
            {
                errors = new IssueValidator(_issueRepo).Validate(issue);
            }
            else if (instance is Project project)
            {
                errors = new ProjectValidator(_projectRepo).Validate(project);
            }

            if (errors.Count > 0)
            {
                throw new CustomValidationException(errors);
            }
            return true;
        }
    }
}
