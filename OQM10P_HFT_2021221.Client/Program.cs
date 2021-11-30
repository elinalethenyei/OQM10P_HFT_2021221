using OQM10P_HFT_2021221.Client.Helper;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace OQM10P_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server...");
            Console.WriteLine("Hit enter when server is loaded!");
            Console.ReadLine();


            var serializerOption = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            Console.WriteLine("\nHit enter to see api call results related to users!");
            Console.ReadLine();
            Console.WriteLine("\n\n**********  USERS  **********\n");
            CallUserEndpoints();

            Console.WriteLine("\nHit enter to see api call results related to projects!");
            Console.ReadLine();
            Console.WriteLine("\n\n**********  PROJECTS  **********\n");
            CallProjectEndpoints();

            Console.WriteLine("\nHit enter to see api call results related to issues!");
            Console.ReadLine();
            Console.WriteLine("\n\n**********  ISSUES  **********\n");
            CallIssueEndpoints();

            Console.WriteLine("\nHit enter to see reports!");
            Console.ReadLine();
            Console.WriteLine("\n\n**********  REPORTS  **********\n");
            
            var result1 = CallReportEndpoints<TopTimeSpentUserByBiggestProjectResponse>("GetTopUserByTopProject", "Tervezett ráfordítás alapján a legnagyobb projekten melyik user dolgozott a legtöbbet");
            Console.WriteLine($"Biggest project by estimated time: {result1.ProjectName}, \nUser who worked on it the most: {result1.UserName}, \nSum time spent on the project by the user: {result1.TimeSpentSum}");

            var result2 = CallReportEndpoints<Dictionary<string, int>>("GetTop3UserByClosedIssues", "Visszaadja azt a top 3 usert, akik a legtöbb issuet zárták le");
            result2.ToConsole("User name", "Sum closed issues");

            var result3 = CallReportEndpoints<TopPriorityIssueSolverProjectOwnerResponse>("GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject", "Visszaadja, hogy ki a tulajdonosa annak a projektnek, ahol a legtöbb magas prioritású feladat a tervezett időn belül lett lezárva");
            Console.WriteLine($"Project name: {result3.ProjectName}, \nNumber of closed high priority issues: {result3.IssueCount}, \nOwner of the project: {result3.OwnerName}");

            var result4 = CallReportEndpoints<Dictionary<UserSexType, int>>("GetDoneIssueCountByUserSexInDueDate", "Visszaadja, hogy nők és férfiak mennyi taskkal végeztek határidőn belül");
            result4.ToConsole("Sex", "Sum closed issues before due date");

            var result5 = CallReportEndpoints<Dictionary<string, double>>("GetSpentPerEstimatedTimeRatePerProject", "Riport ami tartalmazza a projekt nevét és projekt lezárt feladataival eltöltött és becsült idő arányát projektenként");
            result5.ToConsole("Project name", "Time spent/estimated");

            var result6 = CallReportEndpoints<Dictionary<string, int>>("GetTop3ProjectWithFewBugs", "Visszaadja azt a top 3 projektet, ahol a legkevesebb BUG típusú issue található");
            result6.ToConsole("Project name", "Number of BUG issues");

            Console.WriteLine("\n\n***** The End *****");
            Console.ReadLine();



        }

        static IEnumerable<Issue> CallIssueEndpoints()
        {
            var httpService = new HttpService<Issue, int>("Issue", "http://localhost:51332/api/");

            //Issue GetAll
            var issues = httpService.GetAll();
            Console.WriteLine("\n\n***** Issue GetAll api response *****");
            issues.ToConsole();

            //Issue GetOne
            var oneIssue = httpService.Get((int)issues.First().Id);
            Console.WriteLine("\n\n***** Issue Get api response *****");
            Console.WriteLine(oneIssue);

            //Issue Create
            var newIssue = new Issue("Új feladat alapértelmezett beállításokkal") { ProjectId = 1 };
            var createResult = httpService.Create(newIssue);
            Console.WriteLine("\n\n***** Issue Create api response *****");
            createResult.ToConsole("Create");

            //Check Issue Create
            issues = httpService.GetAll();
            Console.WriteLine("\n\n***** Issue GetAll api response for Create check *****");
            issues.ToConsole();

            //Issue Update
            var issueForUpdate = issues.Last();
            issueForUpdate.Status = IssueStatus.INPROGRESS;
            issueForUpdate.UserId = 1;
            var updateResult = httpService.Update(issueForUpdate);
            Console.WriteLine("\n\n***** Issue Update api response *****");
            updateResult.ToConsole("Update");

            //Check Issue Update
            issues = httpService.GetAll();
            Console.WriteLine("\n\n***** Issues GetAll api response for Update check (last record has been updated) *****");
            issues.ToConsole();

            //Issue Delete
            var deleteResult = httpService.Delete((int)issues.Last().Id);
            Console.WriteLine("\n\n***** Issues Delete api response *****");
            deleteResult.ToConsole("Delete");

            //Check Issue Delete
            issues = httpService.GetAll();
            Console.WriteLine("\n\n***** Issues GetAll api response for Delete check (last record has been deleted) *****");
            issues.ToConsole();

            return issues;
        }

        static IEnumerable<User> CallUserEndpoints()
        {
            var httpService = new HttpService<User, int>("User", "http://localhost:51332/api/");

            //User GetAll
            var users = httpService.GetAll();
            Console.WriteLine("\n\n***** User GetAll api response *****");
            users.ToConsole();

            //User GetOne
            var oneUser = httpService.Get((int)users.First().Id);
            Console.WriteLine("\n\n***** User Get api response *****");
            Console.WriteLine(oneUser);

            //User Create
            var newUser = new User("Teszt Elek", "tesztelek", "tesztelek@test.com");
            var createResult = httpService.Create(newUser);
            Console.WriteLine("\n\n***** User Create api response *****");
            createResult.ToConsole("Create");

            //Check User Create
            users = httpService.GetAll();
            Console.WriteLine("\n\n***** User GetAll api response for Create check *****");
            users.ToConsole();

            //User Update
            var userForUpdate = users.Last();
            userForUpdate.Position = UserPositionType.MEDIOR_DEV;
            var updateResult = httpService.Update(userForUpdate);
            Console.WriteLine("\n\n***** User Update api response *****");
            updateResult.ToConsole("Update");

            //Check User Update
            users = httpService.GetAll();
            Console.WriteLine("\n\n***** User GetAll api response for Update check (last record has been updated) *****");
            users.ToConsole();

            //User Delete
            var deleteResult = httpService.Delete((int)users.Last().Id);
            Console.WriteLine("\n\n***** User Delete api response *****");
            deleteResult.ToConsole("Delete");

            //Check User Delete
            users = httpService.GetAll();
            Console.WriteLine("\n\n***** User GetAll api response for Delete check (last record has been deleted) *****");
            users.ToConsole();

            return users;
        }

        static IEnumerable<Project> CallProjectEndpoints()
        {
            var httpService = new HttpService<Project, int>("Project", "http://localhost:51332/api/");

            //Project GetAll
            var projects = httpService.GetAll();
            Console.WriteLine("\n\n***** Project GetAll api response *****");
            projects.ToConsole();

            //Project GetOne
            var oneProject = httpService.Get((int)projects.First().Id);
            Console.WriteLine("\n\n***** Project Get api response *****");
            Console.WriteLine(oneProject);

            //Project Create
            var newProject = new Project("Új teszt projekt", 1);
            var createResult = httpService.Create(newProject);
            Console.WriteLine("\n\n***** Project Create api response *****");
            createResult.ToConsole("Create");

            //Check Project Create
            projects = httpService.GetAll();
            Console.WriteLine("\n\n***** Project GetAll api response for Create check *****");
            projects.ToConsole();

            //Project Update
            var projectForUpdate = projects.Last();
            projectForUpdate.GoalDescription = "Kell valamit csinálni a projekt során, mert eddig nem volt a célnak leírása";
            var updateResult = httpService.Update(projectForUpdate);
            Console.WriteLine("\n\n***** User Update api response *****");
            updateResult.ToConsole("Update");

            //Check Project Update
            projects = httpService.GetAll();
            Console.WriteLine("\n\n***** Project GetAll api response for Update check (last record has been updated) *****");
            projects.ToConsole();

            //Project Delete
            var deleteResult = httpService.Delete((int)projects.Last().Id);
            Console.WriteLine("\n\n***** Project Delete api response *****");
            deleteResult.ToConsole("Delete");

            //Check Project Delete
            projects = httpService.GetAll();
            Console.WriteLine("\n\n***** Project GetAll api response for Delete check (last record has been deleted) *****");
            projects.ToConsole();

            return projects;
        }

        static TResponseType CallReportEndpoints<TResponseType>(string actionName, string reportTitle)
        {
            Console.WriteLine($"\n\n***** {reportTitle} *****");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51332/api/report/");
                var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                var response = client.GetAsync(actionName).GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<TResponseType>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }

        }
    }
}
