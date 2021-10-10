using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class IssueService : IIssueService
    {
        private IIssueRepo _issueRepo;

        public IssueService(IIssueRepo issueRepo)
        {
            _issueRepo = issueRepo;
        }
        public Issue Create(Issue entity)
        {
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
