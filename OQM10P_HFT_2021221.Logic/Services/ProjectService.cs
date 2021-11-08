using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepo _projectRepo;
        private IIssueRepo _issueRepo;

        public ProjectService(IProjectRepo projectRepo, IIssueRepo issueRepo)
        {
            _projectRepo = projectRepo;
            _issueRepo = issueRepo;
        }

        public Project Create(Project entity)
        {
            //check owner csak senior lehet
            return _projectRepo.Create(entity);
            //csak akkor lehet create-en kívül másik státuszt állítani, ha egyből van tulajdonosa a tasknak
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
            //check owner csak senior lehet
            return _projectRepo.Update(entity);
            //modifiedAt kitöltés
            //ha eddig nem done volt a státusz, de most az, akkor closedAt kitöltése
            //csak akkor lehet státuszt váltani, ha van tulajdonosa a tasknak
        }

        //Lekérünk egy riportot arról, hogy az egyes projektek lezárt feladati esetében mi az arány a feladattal eltöltött idő és a feladatra eredetileg becsült idő között
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

            //var result = from project in _projectRepo.ReadAll()
            //             join ratio in ratios
            //                on project.Id equals ratio.ProjectId
            //             select new
            //             {
            //                 ProjectName = project.Name,
            //                 ratio.Ratio
            //             };

            var result = from issue in _issueRepo.ReadAll()
                     where issue.TimeSpent > 0 && issue.EstimatedTime > 0
                     group issue by issue.ProjectId into grouped
                     orderby ((double)grouped.Sum(i => i.TimeSpent) / (double)grouped.Sum(i => i.EstimatedTime))
                     select new
                     {
                         ProjectName = grouped.Key,
                         Ratio = ((double)grouped.Sum(i => i.TimeSpent) / (double)grouped.Sum(i => i.EstimatedTime))
                     };

            return result.ToDictionary(item => item.ProjectName.ToString(), item => item.Ratio);

        }
    }
}
