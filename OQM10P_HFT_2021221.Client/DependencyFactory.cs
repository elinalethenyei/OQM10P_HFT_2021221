using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Logic.Services;
using OQM10P_HFT_2021221.Repository;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Repository.Repositories;

namespace OQM10P_HFT_2021221.Client
{
    public static class DependencyFactory
    {
        static IssueManagementDbContext _dbContext;
        static IIssueRepo _issueRepo;
        static IUserRepo _userRepo;
        static IProjectRepo _projectRepo;


        public static IProjectService GetProjectService()
        {
            if (_dbContext == null)
            {
                _dbContext = new IssueManagementDbContext();
            }

            if (_projectRepo == null)
            {
                _projectRepo = new ProjectRepo(_dbContext);
            }

            return new ProjectService(_projectRepo);
        }

        public static IIssueService GetIssueService()
        {
            if (_dbContext == null)
            {
                _dbContext = new IssueManagementDbContext();
            }

            if (_issueRepo == null)
            {
                _issueRepo = new IssueRepo(_dbContext);
            }

            return new IssueService(_issueRepo);
        }

        public static IUserService GetUserService()
        {
            if (_dbContext == null)
            {
                _dbContext = new IssueManagementDbContext();
            }

            if (_userRepo == null)
            {
                _userRepo = new UserRepo(_dbContext);
            }

            return new UserService(_userRepo);
        }
    }
}
