using Microsoft.AspNetCore.Mvc;
using OQM10P_HFT_2021221.Logic.Interfaces;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using OQM10P_HFT_2021221.Validation.Exceptions;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OQM10P_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseCrudController<TEntity, TKey> : ControllerBase
    {

        readonly IBaseService<TEntity, TKey> service;

        public BaseCrudController(IBaseService<TEntity, TKey> service)
        {
            this.service = service;
        }

        // GET: api/TEntity
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<TEntity> GetAll()
        {
            return service.ReadAll();
        }

        // GET api/TEntity/5
        [HttpGet("{id}")]
        public TEntity Get(TKey id)
        {
            return service.Read(id);
        }

        // POST api/TEntity
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Save([FromBody] TEntity TEntity)
        {
            var result = new ApiResult(true);
            try
            {
                service.Create(TEntity);
            }
            catch (CustomValidationException e)
            {
                result.isSuccess = false;
                result.errorMessages = e.Errors.Select(x => x.ErrorMessage).ToList();
            }
            return result;
        }

        // PUT api/TEntity
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Update([FromBody] TEntity TEntity)
        {
            var result = new ApiResult(true);
            try
            {
                service.Update(TEntity);
            }
            catch (CustomValidationException)
            {
                result.isSuccess = false;
            }
            return result;
        }

        // DELETE api/TEntity/5
        [HttpDelete("{id}")]
        public ApiResult Delete(TKey id)
        {
            var result = new ApiResult(true);
            try
            {
                service.Delete(id);
            }
            catch (CustomValidationException)
            {
                result.isSuccess = false;
            }
            return result;
        }
    }
}
