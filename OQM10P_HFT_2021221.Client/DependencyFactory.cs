using OQM10P_HFT_2021221.Repository;
using OQM10P_HFT_2021221.Repository.Interfaces;

namespace OQM10P_HFT_2021221.Client
{
    public static class DependencyFactory
    {
        static IssueManagementDbContext _dbContext;
        static IIssueRepo _issueRepo;
        static IUserRepo _userRepo;
        static IProjectRepo _projectRepo;

    }
}
