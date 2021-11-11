using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Models.ResponseObjects
{
    public class TopPriorityIssueSolverProjectOwnerResponse
    {
        public string ProjectName { get; set; }
        public int IssueCount { get; set; }
        public string OwnerName { get; set; }

    }
}
