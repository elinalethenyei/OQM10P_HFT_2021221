using OQM10P_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Logic.Interfaces
{
    public interface IIssueService
    {
        IList<Issue> ReadAll();

        Issue Read(int id);

        Issue Create(Issue entity);

        Issue Update(Issue entity);

        void Delete(int id);
    }
}
