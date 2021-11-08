using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System;

namespace OQM10P_HFT_2021221.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepo
    {
        public UserRepository(IssueManagementDbContext context) : base(context)
        {
            Context = context;
        }

        //public override User Read(int key)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
