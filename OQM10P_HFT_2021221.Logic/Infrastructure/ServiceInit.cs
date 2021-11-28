using Microsoft.Extensions.DependencyInjection;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Logic.Services;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Infrastructure;
using OQM10P_HFT_2021221.Validation.Infrastructure;
using OQM10P_HFT_2021221.Validation.Interfaces;
using OQM10P_HFT_2021221.Validation.Validators;

namespace OQM10P_HFT_2021221.Logic.Infrastructure
{
    public static class ServiceInit
    {
        public static void RegisterServices(IServiceCollection services)
        {
            RepoInit.RegisterRepos(services);
            ValidatorInit.RegisterValidators(services);
            services.AddScoped<IModelValidator, ModelValidator>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReportService, ReportService>();
        }
    }
}
