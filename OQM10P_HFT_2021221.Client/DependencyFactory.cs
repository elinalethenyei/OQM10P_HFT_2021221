using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Logic.Services;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Repository.Repositories;
using OQM10P_HFT_2021221.Validation.Interfaces;
using OQM10P_HFT_2021221.Validation.Validators;

namespace OQM10P_HFT_2021221.Client
{
    public static class DependencyFactory
    {
        static IssueManagementDbContext _dbContext;
        static IIssueRepo _issueRepo;
        static IUserRepo _userRepo;
        static IProjectRepo _projectRepo;
        static ModelValidator _validator;
        static IValidator<Issue> _issueValidator;
        static IValidator<Project> _projectValidator;
        static IValidator<User> _userValidator;



        public static IProjectService GetProjectService()
        {
            initEverything();
            return new ProjectService(_projectRepo, _issueRepo, _validator);
        }

        public static IIssueService GetIssueService()
        {
            initEverything();
            return new IssueService(_issueRepo, _validator);
        }

        public static IUserService GetUserService()
        {
            initEverything();
            return new UserService(_userRepo, _projectRepo, _issueRepo , _validator);
        }

        private static void  initEverything()
        {
            if (_dbContext == null)
            {
                _dbContext = new IssueManagementDbContext();
            }

            if (_projectRepo == null)
            {
                _projectRepo = new ProjectRepository(_dbContext);
            }

            if (_issueRepo == null)
            {
                _issueRepo = new IssueRepository(_dbContext);
            }
            if (_userRepo == null)
            {
                _userRepo = new UserRepository(_dbContext);
            }
            if (_projectValidator == null)
            {
                _projectValidator = new ProjectValidator(_projectRepo, _userRepo);
            }
            if (_issueValidator == null)
            {
                _issueValidator = new IssueValidator(_issueRepo, _userRepo, _projectRepo);
            }
            if (_userValidator == null)
            {
                _userValidator = new UserValidator(_userRepo);
            }
            if (_validator == null)
            {
                _validator = new ModelValidator(_userRepo, _issueRepo, _projectRepo, _issueValidator, _projectValidator, _userValidator);
            }

        }


    }
}
