using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post -- Create
        public IHttpActionResult Post(Customer customer)
        {
            // If an empty Customer is passed in
            if (customer == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            // If the ModelState is not Valid
            if (ModelState.IsValid) //If model state is valid, we have successfully passed a customer object
            {

                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest(ModelState);

        }

        //Get -- Read All

        public IHttpActionResult Get()
        {
            List<Customer> customers = _context.Customers.ToList();
            if (customers.Count != 0)
            {
                return Ok(customers);
            }
            return BadRequest("Your database contains no Customers");
        }


        //Get({id} -- Read by ID
        public IHttpActionResult Get(int id)
        {
            
            Customer customer = _context.Customers.Find(id);
            
            //if id is 0
            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        //Put -- Update

        //Delete{id} -- Delete by ID
    }
}
