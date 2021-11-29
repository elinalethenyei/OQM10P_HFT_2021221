using Microsoft.Extensions.Logging;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Logic.Services
{
    public class UserService : IUserService
    {

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory();
        private IUserRepo _userRepo;
        private IProjectRepo _projectRepo;
        private IIssueRepo _issueRepo;
        private IModelValidator _validator;

        public UserService(IUserRepo userRepo, IProjectRepo projectRepo, IIssueRepo issueRepo, IModelValidator validator)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _issueRepo = issueRepo;
            _validator = validator;
        }

        public User Create(User entity)
        {
            _validator.Validate(entity);
            return _userRepo.Create(entity);
        }

        public void Delete(int id)
        {
            //ha projekt tulaja, nem törölhető, előbb a projekt tulaját kell módosítani
            if (_projectRepo.ReadAll().Where(x => x.Id == id).Count() == 0)
            {
                Console.WriteLine($"User does not exists with the given id! Id: {id}");
            }

            if (_projectRepo.ReadAll().Where(x => x.OwnerId == id).Count() > 0)
            {
                Console.WriteLine("User can not be deleted, remove from all projects first!");
            }

            var issuesToUpdate = _issueRepo.ReadAll().Where(x => x.UserId != null && x.UserId == id);
            foreach (Issue issue in issuesToUpdate)
            {
                issue.UserId = null;
                issue.User = null;
                _issueRepo.Update(issue);
            }

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
            _validator.Validate(entity);
            User savedUser = _userRepo.Read((int)entity.Id);
            savedUser.Name = entity.Name;
            savedUser.Position = entity.Position;
            savedUser.Sex = entity.Sex;
            savedUser.Email = entity.Email;
            return _userRepo.Update(savedUser);
        }
    }
}
