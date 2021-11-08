using Microsoft.Extensions.Logging;
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
    public class UserService : IUserService
    {

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory();
        private IUserRepo _userRepo;
        private ModelValidator _validator;

        public UserService(IUserRepo userRepo, ModelValidator validator)
        {
            _userRepo = userRepo;
            _validator = validator;
        }

        public User Create(User entity)
        {
            try
            {
                _validator.Validate(entity);
                return _userRepo.Create(entity);
            } catch(CustomValidationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public void Delete(int id)
        {
            //ha projekt tulaja, nem törölhető, előbb a projekt tulaját kell módosítani
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
            //username nem módosítható
            //unique email check
            return _userRepo.Update(entity);
        }

        //TODO melyik user dolgozott a legtöbbet a legnagyobb projekten
        //TODO statisztika: a nők és férfiak közül melyik milyen arányban végez a taskjaival estimated time-on belül, tehát ahol az arány 1 alatt van
    }
}
