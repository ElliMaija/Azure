using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Entities;
using Microsoft.AspNetCore.JsonPatch;
using CrudWebApi.Filters;

namespace CrudWebApi.Controllers
{
    [TypeFilter(typeof(MyExceptionFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MySqlContext dbc;

        public CustomersController(MySqlContext dbc)
        {
            this.dbc = dbc;
        }

        // GET api/values
        [HttpGet]        
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(dbc.Customers.ToArray());
        }


        // GET api/values/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(int id)
        {
            var found = dbc.Customers.SingleOrDefault(e => e.Id == id);
            if (found != null)
                return Ok(found);
            return NotFound();
        }
        // Patch api/values/5
        [HttpPatch("{id}")]
        public ActionResult<Customer> Update(int id,
        [FromBody]JsonPatchDocument<Customer> value)
        {            
            Customer current = dbc.Customers.SingleOrDefault(e => e.Id == id);
            if (current == null) return NotFound();
            value.ApplyTo(current);
            dbc.SaveChanges();
            return Ok(current);
        }



        // POST api/values

        /// <summary>
        /// Creates a new Customer
        /// </summary>
        /// <param name="value">New Customer.
        /// </param>
        /// <returns>New Customer.</returns>
        /// <response code="201">Returns the new Customer.</response>
        /// <response code="400">If the value is null.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        public ActionResult<Customer> Post([FromBody]Customer value)
        {            
            if (value == null) return BadRequest();
            value.Id = 0;
            dbc.Customers.Add(value);
            dbc.SaveChanges();
            return CreatedAtRoute("GetCustomer",
            new { id = value.Id },
            null);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody]Customer value)
        {
            if (value.Id != id)
                throw new Exception("id ei ole sama kuin value.id");
            Customer current = dbc.Customers.SingleOrDefault(e => e.Id == id);
            if (current == null) return NotFound();
            current.Name = value.Name;
            current.Saldo = value.Saldo;
            dbc.SaveChanges();
            return Ok(current);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Customer current = dbc.Customers.SingleOrDefault(e => e.Id == id);
            if (current == null) return NotFound();
            dbc.Customers.Remove(current);
            dbc.SaveChanges();
            return new NoContentResult();
        }
    }
}
