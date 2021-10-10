using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class UserRepo : RepoBase<User, int>, IUserRepo
    {
        public UserRepo(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        public override User Read(int key)
        {
            throw new NotImplementedException();
        }
    }
}
