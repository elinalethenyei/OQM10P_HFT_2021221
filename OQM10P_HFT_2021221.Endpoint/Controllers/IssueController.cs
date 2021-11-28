using Microsoft.AspNetCore.Mvc;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OQM10P_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssueController : CrudControllerBase<Issue, int>
    {
        public IssueController(IIssueService issueService) : base(issueService)
        {
        }
    }
}
