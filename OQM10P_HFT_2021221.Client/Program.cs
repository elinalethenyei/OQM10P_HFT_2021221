using OQM10P_HFT_2021221.Client.Helper;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace OQM10P_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server...");
            Console.ReadLine();

            var serializerOption = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            using (var client = new HttpClient())
            {
                var httpService = new HttpService<Issue, int>("Issue", "http://localhost:51332/api/");
                
                CallIssueEndpoints();
                CallUserEndpoints();
                CallProjectEndpoints();

                Console.ReadLine();


            }

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
    }
}
