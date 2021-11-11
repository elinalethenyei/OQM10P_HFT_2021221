using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
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

        TopTimeSpentUserByBiggestProjectResponse GetTopUserByTopProject();

        Dictionary<string, int> GetTop3UserByClosedIssues();

        Dictionary<UserSexType, int> GetDoneIssueCountByUserSexInDueDate();
        TopPriorityIssueSolverProjectOwnerResponse GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject();
    }
}
