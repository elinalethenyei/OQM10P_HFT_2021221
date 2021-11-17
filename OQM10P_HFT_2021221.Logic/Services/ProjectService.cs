using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Exceptions;
using OQM10P_HFT_2021221.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepo _projectRepo;
        private IIssueRepo _issueRepo;
        private ModelValidator _validator;

        public ProjectService(IProjectRepo projectRepo, IIssueRepo issueRepo, ModelValidator validator)
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
                Project savedProject = _projectRepo.Read(entity.Id);
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
            if(savedProject != null)
            {
                savedProject.ClosedAt = new DateTime();
                return _projectRepo.Update(savedProject);
            }
            return null;
        }

        /// <summary>
        /// Lekérünk egy riportot arról, hogy az egyes projektek lezárt feladati esetében mi az arány a feladattal eltöltött idő és a feladatra eredetileg becsült idő között
        /// </summary>
        /// <returns>Riport ami tartalmazza a projekt nevét és projekt feladataival eltöltött és becsült idő arányát</returns>
        public Dictionary<string, double> GetSpentPerEstimatedTimeRatePerProject()
        {

            var ratios = (from issue in _issueRepo.ReadAll()
                          where issue.TimeSpent > 0 && issue.EstimatedTime > 0
                          group issue by issue.ProjectId into grouped
                          orderby ((double)grouped.Sum(i => i.TimeSpent) / (double)grouped.Sum(i => i.EstimatedTime))
                          select new
                          {
                              ProjectId = grouped.Key,
                              Ratio = ((double)grouped.Sum(i => i.TimeSpent) / (double)grouped.Sum(i => i.EstimatedTime))
                          });

            var result = from project in _projectRepo.ReadAll()
                         join ratio in ratios
                            on project.Id equals ratio.ProjectId
                         select new
                         {
                             ProjectName = project.Name,
                             ratio.Ratio
                         };


            return result.ToDictionary(item => item.ProjectName, item => item.Ratio);

        }

        public Dictionary<string, int> GetTop3ProjectWithFewBugs()
        {

            var bugCount = (from issue in _issueRepo.ReadAll()
                          where issue.Type.Equals(IssueType.BUG)
                          group issue by issue.ProjectId into grouped
                          orderby grouped.Count() ascending
                          select new
                          {
                              ProjectId = grouped.Key,
                              BugCount = grouped.Count()
                          }).Take(3);

            var result = (from project in _projectRepo.ReadAll()
                         join grouped in bugCount on project.Id equals grouped.ProjectId
                         select new
                         {
                             ProjectName = project.Name,
                             grouped.BugCount
                         }).ToDictionary(item => item.ProjectName, item => item.BugCount);


            return result;

        }
    }
}
