using OQM10P_HFT_2021221.Models;
using System;

namespace OQM10P_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var userService = DependencyFactory.GetUserService();
            var projectService = DependencyFactory.GetProjectService();
            var issueService = DependencyFactory.GetIssueService();

            Console.WriteLine("User list:");
            foreach (var user in userService.ReadAll())
            {
                Console.WriteLine(user.ToString());
            }

            Console.WriteLine("\r\nProject list:");
            foreach (var project in projectService.ReadAll())
            {
                Console.WriteLine(project.ToString());
            }

            Console.WriteLine("\r\nIssue list:");
            foreach (var issue in issueService.ReadAll())
            {
                Console.WriteLine(issue.ToString());
            }

            Console.WriteLine("\r\nOne project:");
            Console.WriteLine(projectService.Read(3).ToString());

            Console.WriteLine("\r\nEstimated per actually spent time per project report");
            foreach (var reportRow in projectService.GetSpentPerEstimatedTimeRatePerProject())
            {
                Console.WriteLine($"Project name: {reportRow.Key}, \t\t Spent/Estimated ratio: {reportRow.Value}");
            }

            userService.Create(new User
            {
                Name = "asd",
                //Username= "asd",
                Email ="asd",
                Sex = UserSexType.FEMALE,
                Position = UserPositionType.MEDIOR_DEV
            });
            Console.WriteLine(userService.GetTopUserByTopProject());
            userService.GetTop3UserByClosedIssues();
            projectService.GetTop3ProjectWithFewBugs();
            userService.GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject();
            userService.GetDoneIssueCountByUserSexInDueDate();
        }
    }
}
