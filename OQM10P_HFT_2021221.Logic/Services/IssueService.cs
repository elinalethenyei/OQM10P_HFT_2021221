using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using OQM10P_HFT_2021221.Validation.Validators;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class IssueService : IIssueService
    {
        private IIssueRepo _issueRepo;
        private ModelValidator _validator;

        public IssueService(IIssueRepo issueRepo, ModelValidator validator)
        {
            _issueRepo = issueRepo;
            _validator = validator;
        }

        public Issue Create(Issue entity) 
        {
            try
            {
                _validator.Validate(_issueRepo);
            }
            catch (CustomValidationException e)
            {
            }
            return _issueRepo.Create(entity);
        }

        public void Delete(int id)
        {
            _issueRepo.Delete(id);
        }

        public Issue Read(int id)
        {
            return _issueRepo.Read(id);
        }

        public IList<Issue> ReadAll()
        {
            return _issueRepo.ReadAll().ToList();
        }

        public Issue Update(Issue entity)
        {
            return _issueRepo.Update(entity);
        }
    }
}
