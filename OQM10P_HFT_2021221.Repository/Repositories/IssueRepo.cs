using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class IssueRepo : RepoBase<Issue, int>, IIssueRepo
    {
        public IssueRepo(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        public override Issue Read(int key)
        {
            return null;
        }
    }
}
