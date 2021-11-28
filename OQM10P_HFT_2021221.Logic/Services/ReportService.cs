using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class ReportService : IReportService
    {

        private IUserRepo _userRepo;
        private IProjectRepo _projectRepo;
        private IIssueRepo _issueRepo;

        public ReportService(IUserRepo userRepo, IProjectRepo projectRepo, IIssueRepo issueRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _issueRepo = issueRepo;
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
                                }).FirstOrDefault().ProjectId;

            var topUser = (from issue in _issueRepo.ReadAll()
                           where issue.ProjectId == topProjectId && (issue.UserId != null)
                           group issue by issue.User.Username into grouped
                           orderby grouped.Sum(i => i.TimeSpent) descending
                           select new TopTimeSpentUserByBiggestProjectResponse
                           {
                               UserName = grouped.Key,
                               TimeSpentSum = grouped.Sum(i => i.TimeSpent)
                           }).FirstOrDefault();

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
                          }).FirstOrDefault();
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

        /// <summary>
        /// Lekérünk egy riportot arról, hogy az egyes projektek lezárt feladati esetében mi az arány a feladattal eltöltött idő és a feladatra eredetileg becsült idő között
        /// </summary>
        /// <returns>Riport ami tartalmazza a projekt nevét és projekt feladataival eltöltött és becsült idő arányát</returns>
        public Dictionary<string, double> GetSpentPerEstimatedTimeRatePerProject()
        {

            var ratios = (from issue in _issueRepo.ReadAll()
                          where issue.TimeSpent > 0 && issue.EstimatedTime > 0
                          group issue by issue.ProjectId into grouped
                          orderby ((double)grouped.Sum(i => i.TimeSpent) / (double)grouped.Sum(i => i.EstimatedTime))
                          select new
                          {
                              ProjectId = grouped.Key,
                              Ratio = ((double)grouped.Sum(i => i.TimeSpent) / (double)grouped.Sum(i => i.EstimatedTime))
                          });

            var result = from project in _projectRepo.ReadAll()
                         join ratio in ratios
                            on project.Id equals ratio.ProjectId
                         select new
                         {
                             ProjectName = project.Name,
                             ratio.Ratio
                         };


            return result.ToDictionary(item => item.ProjectName, item => item.Ratio);

        }

        /// <summary>
        /// Lekéri azt a top 3 projektet, ahol a legkevesebb BUG típusú issue található
        /// </summary>
        /// <returns>Projektenkénti BUG típusú issuek számát tartalmazó Dictionary</returns>
        public Dictionary<string, int> GetTop3ProjectWithFewBugs()
        {

            var bugCount = (from issue in _issueRepo.ReadAll()
                            where issue.Type.Equals(IssueType.BUG)
                            group issue by issue.ProjectId into grouped
                            orderby grouped.Count() ascending
                            select new
                            {
                                ProjectId = grouped.Key,
                                BugCount = grouped.Count()
                            }).Take(3);

            var result = (from project in _projectRepo.ReadAll()
                          join grouped in bugCount on project.Id equals grouped.ProjectId
                          select new
                          {
                              ProjectName = project.Name,
                              grouped.BugCount
                          }).ToDictionary(item => item.ProjectName, item => item.BugCount);

            return result;

        }

    }
}
