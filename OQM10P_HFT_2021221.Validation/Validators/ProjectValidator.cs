using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OQM10P_HFT_2021221.Validation.Validators
{
    public class ProjectValidator : IValidator<Project>
    {
        private IProjectRepo _projectRepo;
        private IUserRepo _userRepo;

        public ProjectValidator(IProjectRepo projectRepo, IUserRepo userRepo)
        {
            _projectRepo = projectRepo;
            _userRepo = userRepo;
        }

        public List<ValidationResult> Validate(Project project)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var vc = new ValidationContext(project);
            Validator.TryValidateObject(project, vc, errors, validateAllProperties: true);

            if (project.Id != null)
            {
                Project savedProject = _projectRepo.Read((int)project.Id);
                if (savedProject == null)
                {
                    errors.Add(new ValidationResult($"Project with the given id does not exists! Id: {project.Id}"));
                }
            }
            if (project.Name != null && _projectRepo.ReadAll().Where(x => x.Name.Equals(project.Name) && !x.Id.Equals(project.Id)).Count() > 0)
            {
                errors.Add(new ValidationResult($"Project name already exists! Project name: {project.Name}"));
            }
            if(project.OwnerId != null && _userRepo.Read((int)project.OwnerId) == null)
            {
                errors.Add(new ValidationResult($"User does not exist with the given project owner id! Owner id: {project.OwnerId}"));
            }

            return errors;
        }


    }
}
