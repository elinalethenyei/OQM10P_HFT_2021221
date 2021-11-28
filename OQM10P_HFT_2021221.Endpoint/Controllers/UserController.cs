using Microsoft.AspNetCore.Mvc;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;

namespace OQM10P_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseCrudController<User, int>
    {
        public UserController(IUserService userService) : base(userService)
        {
        }
    }
}
