using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class ProjectRepo : RepoBase<Project, int>, IProjectRepo
    {
        public ProjectRepo(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        public override Project Read(int key)
        {
            throw new NotImplementedException();
        }
    }
}
