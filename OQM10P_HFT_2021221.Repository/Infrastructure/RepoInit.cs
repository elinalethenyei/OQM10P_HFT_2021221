using Microsoft.Extensions.DependencyInjection;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Repository.Repositories;

namespace OQM10P_HFT_2021221.Repository.Infrastructure
{
    public static class RepoInit
    {
        public static void RegisterRepos(IServiceCollection services)
        {
            services.AddScoped((_) => new IssueManagementDbContext());
            services.AddScoped<IIssueRepo, IssueRepository>();
            services.AddScoped<IProjectRepo, ProjectRepository>();
            services.AddScoped<IUserRepo, UserRepository>();
        }
    }
}
