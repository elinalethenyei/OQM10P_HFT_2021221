using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class ProjectRepository : RepositoryBase<Project, int>, IProjectRepo
    {
        public ProjectRepository(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        //public override Project Read(int key)
        //{
        //    return Context.Find<Project>(key);
        //}
    }
}
