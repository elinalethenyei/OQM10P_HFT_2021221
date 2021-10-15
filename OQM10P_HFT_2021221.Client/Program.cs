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
                //Console.WriteLine($"Id: {user.Id}, {user.Name}");
                Console.WriteLine(user.ToString());
            }

            Console.WriteLine("\r\nProject list:");
            foreach (var project in projectService.ReadAll())
            {
                //Console.WriteLine($"Id: {project.Id}, {project.Name}");
                Console.WriteLine(project.ToString());
            }

            Console.WriteLine("\r\nIssue list:");
            foreach (var issue in issueService.ReadAll())
            {
                Console.WriteLine(issue.ToString());
            }
        }
    }
}
