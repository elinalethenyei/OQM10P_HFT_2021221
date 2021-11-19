using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepo _projectRepo;
        private IIssueRepo _issueRepo;
        private IModelValidator _validator;

        public ProjectService(IProjectRepo projectRepo, IIssueRepo issueRepo, IModelValidator validator)
        {
            _projectRepo = projectRepo;
            _issueRepo = issueRepo;
            _validator = validator;
        }

        public Project Create(Project entity)
        {
            try
            {
                _validator.Validate(entity);
                return _projectRepo.Create(entity);
            }
            catch (CustomValidationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Delete(int id)
        {
            _projectRepo.Delete(id);
        }

        public Project Read(int id)
        {
            return _projectRepo.Read(id);
        }

        public IList<Project> ReadAll()
        {
            return _projectRepo.ReadAll().ToList();
        }

        public Project Update(Project entity)
        {
            try
            {
                _validator.Validate(entity);
                Project savedProject = _projectRepo.Read((int)entity.Id);
                savedProject.OwnerId = entity.OwnerId;
                savedProject.Name = entity.Name;
                savedProject.EstimatedTime = entity.EstimatedTime;
                savedProject.GoalDescription = entity.GoalDescription;
                entity.ModifiedAt = new DateTime();
                return _projectRepo.Update(savedProject);
            }
            catch (CustomValidationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Project closeProject(int id)
        {
            Project savedProject = _projectRepo.Read(id);
            if (savedProject != null)
            {
                savedProject.ClosedAt = new DateTime();
                return _projectRepo.Update(savedProject);
            }
            return null;
        }


    }
}
