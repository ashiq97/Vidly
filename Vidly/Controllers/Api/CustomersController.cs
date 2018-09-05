using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;


namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers  => for list of customers that will response like this
        //public IEnumerable<CustomerDto> GetCustomers()
        //{
        //    // Now map the customer object to customer 
        //    return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        //}
       /* public IHttpActionResult GetCustomer()
        {
            var customerDtos = _context.Customers
                .Include(c => c.MemberShipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }*/

         public IHttpActionResult GetCustomer(string query = null ) // for 123th video
        {
            var customersQuery = _context.Customers
                .Include(c => c.MemberShipType);
               
             if(!String.IsNullOrWhiteSpace(query))
                 customersQuery = customersQuery.Where(c=>c.Name.Contains(query));
             
            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }


        // GET /api/customers/1   => for single customer that will response like this

        public IHttpActionResult GetCustomer(int id)
        {

            var customer =  _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            //return customer;
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        // POST / api/customers  => we post a customer to customer Collection

        [HttpPost] // because we creating a resource // this action will call when a http post resquest is sent
        public IHttpActionResult CreateCustomer(CustomerDto  customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" +customer.Id),customerDto);
        }


        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id,CustomerDto customerDto) // id is read by url and customer is come form request 
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            // otherwirse we need to update the customer 

            Mapper.Map(customerDto,customerInDb);
            _context.SaveChanges();

        }
 
        /// so this is how we map objects with auto mapper

        //DELETE api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb); // so the object will mark as remove in memory
            _context.SaveChanges();
        }


        ////// so this is how you built apis with asp.net web api frameWork
    }
}
