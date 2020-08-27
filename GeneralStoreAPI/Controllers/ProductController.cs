using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();


        //Post
        public IHttpActionResult Post(Product product)
        {
            if (product == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            product.SKU = GenerateSku(product.Name);
            // If the ModelState is not Valid
            if (ModelState.IsValid && product.SKU != null) //If model state is valid, we have successfully passed a product object
            {

                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest(ModelState);
        }

        //Get -- Read all
        public IHttpActionResult Get()
        {
            List<Product> products = _context.Products.ToList();
            if (products.Count != 0)
            {
                return Ok(products);
            }
            return BadRequest("Your database contains no Products");
        }

        //Get({id}
        public IHttpActionResult Get(string sku)
        {

            Product product = _context.Products.Find(sku);

            //if id is 0
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //Put

        //Delete{id}







        private string GenerateSku(string productName)
        {
            // Initialize a Random object so we can randomly assign this a number
            Random random = new Random();
            // Get a Random number and turn it into a string
            var randItemNum = random.Next(0, 1000).ToString();
            // Construct a 3 character string based on the above number. If the number is less than 3 digits, add 0's in front of it to make it 3 digits
            var itemId = new string('0', 3 - randItemNum.Length) + randItemNum;
            // Create the entire SKU and return it
            return $"EFA-{productName.Substring(0, 3)}-{itemId}";
        }
    }
}
