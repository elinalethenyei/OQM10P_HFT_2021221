using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Models.ResponseObjects
{
    public class TopTimeSpentUserByBiggestProjectResponse
    {
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public int TimeSpentSum { get; set; }
    }
}
