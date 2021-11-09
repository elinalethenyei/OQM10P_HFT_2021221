﻿using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System;
using System.Linq;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class ProjectRepository : RepositoryBase<Project, int>, IProjectRepo
    {
        public ProjectRepository(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        public override Project Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
