using Microsoft.Extensions.Logging;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using OQM10P_HFT_2021221.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class UserService : IUserService
    {

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory();
        private IUserRepo _userRepo;
        private IProjectRepo _projectRepo;
        private IIssueRepo _issueRepo;
        private ModelValidator _validator;

        public UserService(IUserRepo userRepo, IProjectRepo projectRepo, IIssueRepo issueRepo, ModelValidator validator)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _issueRepo = issueRepo;
            _validator = validator;
        }

        public User Create(User entity)
        {
            try
            {
                _validator.Validate(entity);
                return _userRepo.Create(entity);
            }
            catch (CustomValidationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public void Delete(int id)
        {
            //ha projekt tulaja, nem törölhető, előbb a projekt tulaját kell módosítani
            if(_projectRepo.ReadAll().Where(x => x.Id == id).Count() == 0)
            {
                Console.WriteLine($"User does not exists with the given id! Id: {id}");
            }

            if(_projectRepo.ReadAll().Where(x=>x.OwnerId == id).Count() > 0)
            {
                Console.WriteLine("User can not be deleted, remove from all projects first!");
            }

            _userRepo.Delete(id);
        }

        public User Read(int id)
        {
            return _userRepo.Read(id);
        }

        public IList<User> ReadAll()
        {
            return _userRepo.ReadAll().ToList();
        }

        public User Update(User entity)
        {
            try
            {
                _validator.Validate(entity);
                User savedUser = _userRepo.Read(entity.Id);
                savedUser.Name = entity.Name;
                savedUser.Position = entity.Position;
                savedUser.Sex = entity.Sex;
                savedUser.Email = entity.Email;
                return _userRepo.Update(savedUser);
            }
            catch (CustomValidationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Tervezett ráfordítás alapján a legnagyobb projekten melyik user dolgozott a legtöbbet
        /// </summary>
        /// <returns>top user</returns>
        public TopTimeSpentUserByBiggestProjectResponse GetTopUserByTopProject()
        {
            var topProjectId = (from issue in _issueRepo.ReadAll()
                               where issue.EstimatedTime > 0
                               group issue by issue.ProjectId into grouped
                               orderby grouped.Sum(i => i.EstimatedTime) descending
                               select new
                               {
                                   ProjectId = grouped.Key
                               }).First().ProjectId;

            var topUser = (from issue in _issueRepo.ReadAll()
                          where issue.ProjectId == topProjectId && (issue.UserId  != null)
                          group issue by issue.User.Username into grouped
                          orderby grouped.Sum(i => i.TimeSpent) descending
                          select new TopTimeSpentUserByBiggestProjectResponse
                          {
                              UserName = grouped.Key,
                              TimeSpentSum = grouped.Sum(i => i.TimeSpent)
                          }).First();

            var project = _projectRepo.ReadAll().Where(x => x.Id == topProjectId).First();
            topUser.ProjectName = project.Name;

            return topUser;

        }

        /// <summary>
        /// Visszaadja azt a top 3 usert, akik a legtöbb issuet zárták le
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetTop3UserByClosedIssues()
        {
            var groupedIssuesByUsers = (from issue in _issueRepo.ReadAll()
                                where issue.Status.Equals(IssueStatus.DONE)
                                group issue by issue.UserId into grouped
                                orderby grouped.Count() descending
                                select new
                                {
                                    UserId = grouped.Key,
                                    IssueSum = grouped.Count()
                                }).Take(3);

            var result = (from user in _userRepo.ReadAll()
                          join grouped in groupedIssuesByUsers on user.Id equals grouped.UserId
                          orderby grouped.IssueSum descending
                          select new
                          {
                              UserName = user.Name,
                              grouped.IssueSum
                          }).ToDictionary(item => item.UserName, item => item.IssueSum);

            return result;

        }

        /// <summary>
        /// Visszaadja, hogy ki a tulajdonosa tulajdonosa annak a projektnek, ahol a legtöbb magas prioritású feladat a tervezett időn belül lett megoldva
        /// </summary>
        /// <returns></returns>
        public TopPriorityIssueSolverProjectOwnerResponse GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject()
        {
            var topPrioIssuesByProjectInTime = (from issue in _issueRepo.ReadAll()
                                               where issue.Priority.Equals(IssuePriorityType.HIGH) && issue.Status.Equals(IssueStatus.DONE) && issue.EstimatedTime >= issue.TimeSpent
                                               group issue by issue.ProjectId into grouped
                                               orderby grouped.Count() descending
                                               select new
                                               {
                                                   ProjectId = grouped.Key,
                                                   IssueCount = grouped.Count()
                                               }).Take(1);

            var result = (from project in _projectRepo.ReadAll()
                         join grouped in topPrioIssuesByProjectInTime on project.Id equals grouped.ProjectId
                         select new TopPriorityIssueSolverProjectOwnerResponse
                         {
                             ProjectName = project.Name,
                             IssueCount = grouped.IssueCount,
                             OwnerName = project.Owner.Name
                         }).First();
            return result;

        }

        /// <summary>
        /// A nők és férfiak mennyi taskkal végeztek határidőn belül
        /// </summary>
        /// <returns>Nemek szerint a határidőn belül lezárt taskok számát tartalmazó Dictionary</returns>
        public Dictionary<UserSexType, int> GetDoneIssueCountByUserSexInDueDate()
        {
            var issueCountByUserSex = (from issue in _issueRepo.ReadAll()
                                                where issue.Status.Equals(IssueStatus.DONE) && issue.ClosedAt < issue.DueDate
                                                group issue by issue.User.Sex into grouped
                                                orderby grouped.Count() descending
                                                select new
                                                {
                                                    UserSex = grouped.Key,
                                                    IssueCount = grouped.Count()
                                                }).ToDictionary(item => item.UserSex, item => item.IssueCount);
            return issueCountByUserSex;
        }
    }
}
