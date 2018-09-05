using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        // implimentation rental api
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRental(NewRentalDto newRental)
        {
           // edge case 1
           

            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            // edge case 2
            


            var movies = _context.Movies
                                 .Where(m => newRental.MovieIds
                                            .Contains(m.Id)).ToList();
            // in movies quires turnes into => select*from movies Where Id in (1,2,3..);
 
            // edge case 3
          
            foreach (var movie in movies)
            {
                //edge case 4
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not Available.");

                movie.NumberAvailable--;
                
                // now create a rental Object
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();

                return Ok();
        }

        // end of implementation of rental api
    }
}
