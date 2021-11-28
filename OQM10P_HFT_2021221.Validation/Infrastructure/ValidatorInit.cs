using Microsoft.Extensions.DependencyInjection;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Validation.Interfaces;
using OQM10P_HFT_2021221.Validation.Validators;

namespace OQM10P_HFT_2021221.Validation.Infrastructure
{
    public static class ValidatorInit
    {
        public static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<Issue>, IssueValidator>();
            services.AddScoped<IValidator<Project>, ProjectValidator>();
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IModelValidator, ModelValidator>();
        }
    }
}
