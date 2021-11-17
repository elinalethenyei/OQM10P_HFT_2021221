using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    public class IssueValidator : IValidator<Issue>
    {
        private IIssueRepo _issueRepo;
        private IUserRepo _userRepo;
        private IProjectRepo _projectRepo;

        public IssueValidator(IIssueRepo issueRepo, IUserRepo userRepo, IProjectRepo projectRepo)
        {
            _issueRepo = issueRepo;
            _userRepo = userRepo;
            _projectRepo = projectRepo;
        }

        public List<ValidationResult> Validate(Issue issue)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var vc = new ValidationContext(issue);
            Validator.TryValidateObject(issue, vc, errors, validateAllProperties: true);

            if (issue.Id > 0)
            {
                Issue savedIssue = _issueRepo.Read(issue.Id);
                if (savedIssue == null)
                {
                    errors.Add(new ValidationResult($"Issue with the given id does not exists! Id: {issue.Id}"));
                }
            }
            if(issue.UserId == null && !issue.Status.Equals(IssueStatus.TODO))
            {
                errors.Add(new ValidationResult($"Issue without user has to be in TODO status!"));
            }
            if(issue.UserId != null)
            {
                if (_userRepo.Read((int)issue.UserId) == null)
                {
                    errors.Add(new ValidationResult($"Issue\'s user does not exist with the given id! User id: {issue.ProjectId}"));
                }
            }
            if (_projectRepo.Read(issue.ProjectId) == null)
            {
                errors.Add(new ValidationResult($"Issue\'s project does not exist with the given id! Project id: {issue.ProjectId}"));
            }

            return errors;
        }
    }
}
