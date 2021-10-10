using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public User Create(User entity)
        {
            return _userRepo.Create(entity);
        }

        public void Delete(int id)
        {
            _userRepo.Delete(id);
        }

        public User Read(int id)
        {
            return _userRepo.Read(id);
        }

        public IList<User> ReadAll()
        {
            return _userRepo.ReadAll().ToList();
        }

        public User Update(User entity)
        {
            return _userRepo.Update(entity);
        }
    }
}
