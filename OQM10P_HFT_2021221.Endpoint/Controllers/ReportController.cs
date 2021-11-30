using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
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
        [OpenApiOperation("Visszaadja, hogy a tervezett ráfordítás alapján a legnagyobb projekten melyik user dolgozott a legtöbbet", "")]
        [SwaggerResponse(typeof(TopTimeSpentUserByBiggestProjectResponse))]
        public TopTimeSpentUserByBiggestProjectResponse GetTopUserByTopProject()
        {
            return reportService.GetTopUserByTopProject();
        }

        // GET: api/Reports/GetTop3UserByClosedIssues
        [HttpGet]
        [ActionName("GetTop3UserByClosedIssues")]
        [OpenApiOperation("Visszaadja azt a top 3 felhasználót, akik a legtöbb feladatot zárták le", "")]
        [SwaggerResponse(typeof(Dictionary<string, int>))]
        public Dictionary<string, int> GetTop3UserByClosedIssues()
        {
            return reportService.GetTop3UserByClosedIssues();
        }

        // GET: api/Reports/GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject
        [HttpGet]
        [ActionName("GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject")]
        [OpenApiOperation("Visszaadja, hogy ki a tulajdonosa annak a projektnek, ahol a legtöbb magas prioritású feladat a tervezett időn belül lett lezárva", "")]
        public TopPriorityIssueSolverProjectOwnerResponse GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject()
        {
            return reportService.GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject();
        }

        // GET: api/Reports/GetDoneIssueCountByUserSexInDueDate
        [HttpGet]
        [ActionName("GetDoneIssueCountByUserSexInDueDate")]
        [OpenApiOperation("Visszaadja nemek szerint a határidőn belül lezárt taskok számát", "")]
        public Dictionary<UserSexType, int> GetDoneIssueCountByUserSexInDueDate()
        {
            return reportService.GetDoneIssueCountByUserSexInDueDate();
        }

        // GET: api/Reports/GetSpentPerEstimatedTimeRatePerProject
        [HttpGet]
        [ActionName("GetSpentPerEstimatedTimeRatePerProject")]
        [OpenApiOperation("Visszaadja a projektek nevét és az egyes projekt feladataival eltöltött és becsült idő arányát", "")]
        public Dictionary<string, double> GetSpentPerEstimatedTimeRatePerProject()
        {
            return reportService.GetSpentPerEstimatedTimeRatePerProject();
        }

        // GET: api/Reports/GetTop3ProjectWithFewBugs
        [HttpGet]
        [ActionName("GetTop3ProjectWithFewBugs")]
        [OpenApiOperation("Visszaadja azt a top 3 projektet, ahol a legkevesebb BUG típusú issue található, a válasz tartalmazza a bugok számát is", "")]
        public Dictionary<string, int> GetTop3ProjectWithFewBugs()
        {
            return reportService.GetTop3ProjectWithFewBugs();
        }

    }
}
