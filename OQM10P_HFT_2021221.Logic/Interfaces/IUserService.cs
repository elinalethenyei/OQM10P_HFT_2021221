﻿using OQM10P_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Logic.Interfaces
{
    public interface IUserService
    {
        IList<User> ReadAll();

        User Read(int id);

        User Create(User entity);

        User Update(User entity);

        void Delete(int id);
    }
}