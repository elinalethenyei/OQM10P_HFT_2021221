using OQM10P_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Logic.Interfaces
{
    public interface IProjectService
    {
        IList<Project> ReadAll();

        Project Read(int id);

        Project Create(Project entity);

        Project Update(Project entity);

        void Delete(int id);
        
    }
}
