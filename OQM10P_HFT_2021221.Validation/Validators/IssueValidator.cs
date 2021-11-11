using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    class IssueValidator : IValidator<Issue>
    {
        private IIssueRepo _issueRepo;

        public IssueValidator(IIssueRepo issueRepo)
        {
            _issueRepo = issueRepo;
        }

        public List<ValidationResult> Validate(Issue issue)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var vc = new ValidationContext(issue);
            Validator.TryValidateObject(issue, vc, errors, validateAllProperties: true);

            if (issue.Id > 0)
            {
                Issue savedUser = _issueRepo.Read(issue.Id);
                if (savedUser == null)
                {
                    errors.Add(new ValidationResult($"Issue with the given id does not exists! Id: {issue.Id}"));
                }
            }

            return errors;
        }
    }
}
