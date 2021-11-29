using Microsoft.AspNetCore.Mvc;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using OQM10P_HFT_2021221.Validation.Exceptions;
using System.Linq;

namespace OQM10P_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : BaseCrudController<Project, int>
    {
        IProjectService _projectService;
        public ProjectController(IProjectService projectService) : base(projectService)
        {
            this._projectService = projectService;
        }

        // Close project api/TEntity/Close/5
        [HttpDelete("{id}")]
        [ActionName("Close")]
        public ApiResult Close(int id)
        {
            var result = new ApiResult(true);
            try
            {
                _projectService.CloseProject(id);
            }
            catch (CustomValidationException e)
            {
                result.isSuccess = false;
                result.errorMessages = e.Errors.Select(x => x.ErrorMessage).ToList();
            }
            return result;
        }
    }
}
