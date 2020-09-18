using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CrudSampleContext _crudSampleContext;
        public CustomerController(CrudSampleContext crudSampleContext)
        {
            this._crudSampleContext = crudSampleContext;
        }

        // GET api/values  
        [HttpGet]
        public ActionResult<List<Customer>> Get()
        {
            var itemLst = _crudSampleContext.customers.ToList();
            return new List<Customer>(itemLst);
        }

        // POST api/values  
        [HttpPost]
        public void Post([FromBody] Customer createSample)
        {
            _crudSampleContext.customers.Add(createSample);
            _crudSampleContext.SaveChanges();
        }

        // PUT api/values/5  
        [HttpPut]
        public void Put([FromBody] Customer updateSample)
        {
            _crudSampleContext.customers.Update(updateSample);
            _crudSampleContext.SaveChanges();
        }

        // DELETE api/values/5  
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var itemToDelete = _crudSampleContext.customers.Where(x => x.Id == id).FirstOrDefault();
            _crudSampleContext.customers.Remove(itemToDelete);
            _crudSampleContext.SaveChanges();
        }
    }
}
