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

        public ProjectService(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public Project Create(Project entity)
        {
            return _projectRepo.Create(entity);
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
            return _projectRepo.Update(entity);
        }
    }
}
