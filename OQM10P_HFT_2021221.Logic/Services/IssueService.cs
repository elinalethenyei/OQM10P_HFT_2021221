using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class IssueService : IIssueService
    {
        private IIssueRepo _issueRepo;
        private IModelValidator _validator;

        public IssueService(IIssueRepo issueRepo, IModelValidator validator)
        {
            _issueRepo = issueRepo;
            _validator = validator;
        }

        public Issue Create(Issue entity)
        {
            _validator.Validate(entity);
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
            _validator.Validate(entity);
            Issue savedIssue = _issueRepo.Read((int)entity.Id);
            savedIssue.ModifiedAt = new System.DateTime();
            if (!savedIssue.Status.Equals(IssueStatus.DONE) && entity.Status.Equals(IssueStatus.DONE))
            {
                savedIssue.ClosedAt = savedIssue.ModifiedAt;
            }
            savedIssue.Status = entity.Status;
            savedIssue.Priority = entity.Priority;
            savedIssue.TimeSpent = entity.TimeSpent;
            savedIssue.Title = entity.Title;
            savedIssue.Description = entity.Description;
            savedIssue.DueDate = entity.DueDate;
            savedIssue.EstimatedTime = entity.EstimatedTime;
            savedIssue.Type = entity.Type;
            savedIssue.UserId = entity.UserId;
            savedIssue.ProjectId = entity.ProjectId;
            return _issueRepo.Update(savedIssue);
        }
    }
}
