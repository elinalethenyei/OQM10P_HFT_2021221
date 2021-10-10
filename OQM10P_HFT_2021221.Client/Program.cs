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

            foreach (var user in userService.ReadAll())
            {
                Console.WriteLine("User list:");
                Console.WriteLine($"Id: {user.Id}, {user.Name}");
            }

            foreach (var project in projectService.ReadAll())
            {
                Console.WriteLine("\r\nProject list:");
                Console.WriteLine($"Id: {project.Id}, {project.Name}");
            }

            foreach (var issue in issueService.ReadAll())
            {
                Console.WriteLine("\r\nIssue list:");
                Console.WriteLine($"Id: {issue.Id}, {issue.Title}");
            }
        }
    }
}
