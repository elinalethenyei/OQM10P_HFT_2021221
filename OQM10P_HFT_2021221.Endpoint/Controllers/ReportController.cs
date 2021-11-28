using Microsoft.AspNetCore.Mvc;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using System.Collections.Generic;

namespace OQM10P_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        // GET: api/Reports/GetTopUserByTopProject
        [HttpGet]
        [ActionName("GetTopUserByTopProject")]
        public TopTimeSpentUserByBiggestProjectResponse GetTopUserByTopProject()
        {
            return reportService.GetTopUserByTopProject();
        }

        // GET: api/Reports/GetTop3UserByClosedIssues
        [HttpGet]
        [ActionName("GetTop3UserByClosedIssues")]
        public Dictionary<string, int> GetTop3UserByClosedIssues()
        {
            return reportService.GetTop3UserByClosedIssues();
        }

        // GET: api/Reports/GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject
        [HttpGet]
        [ActionName("GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject")]
        public TopPriorityIssueSolverProjectOwnerResponse GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject()
        {
            return reportService.GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject();
        }

        // GET: api/Reports/GetDoneIssueCountByUserSexInDueDate
        [HttpGet]
        [ActionName("GetDoneIssueCountByUserSexInDueDate")]
        public Dictionary<UserSexType, int> GetDoneIssueCountByUserSexInDueDate()
        {
            return reportService.GetDoneIssueCountByUserSexInDueDate();
        }

        // GET: api/Reports/GetSpentPerEstimatedTimeRatePerProject
        [HttpGet]
        [ActionName("GetSpentPerEstimatedTimeRatePerProject")]
        public Dictionary<string, double> GetSpentPerEstimatedTimeRatePerProject()
        {
            return reportService.GetSpentPerEstimatedTimeRatePerProject();
        }

        // GET: api/Reports/GetTop3ProjectWithFewBugs
        [HttpGet]
        [ActionName("GetTop3ProjectWithFewBugs")]
        public Dictionary<string, int> GetTop3ProjectWithFewBugs()
        {
            return reportService.GetTop3ProjectWithFewBugs();
        }

    }
}
