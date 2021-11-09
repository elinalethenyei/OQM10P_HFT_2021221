using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System.Linq;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class IssueRepository : RepositoryBase<Issue, int>, IIssueRepo
    {
        public IssueRepository(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        public override Issue Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
