using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    class ProjectValidator : IValidator<Project>
    {
        private IProjectRepo _projectRepo;

        public ProjectValidator(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public List<ValidationResult> Validate(Project project)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var vc = new ValidationContext(project);
            Validator.TryValidateObject(project, vc, errors, validateAllProperties: true);

            if (project.Id > 0)
            {
                Project savedProject = _projectRepo.Read(project.Id);
                if (savedProject == null)
                {
                    errors.Add(new ValidationResult($"Project with the given id does not exists! Id: {project.Id}"));
                }
            }
            if (_projectRepo.ReadAll().Count(x => x.Name.Equals(project.Name) && x.Id != project.Id) > 0)
            {
                errors.Add(new ValidationResult($"Project name already exists! Email: {project.Name}"));
            }

            return errors;
        }


    }
}
