using System;
using System.Linq;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;

namespace Vidly.App.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRetals(NewRentalDto newRental)
        {
            var rental = new Rental();

            var lastRental = _context.Rentals
                .OrderByDescending(c => c.Id)
                .FirstOrDefault();

            string rentalNo;

            if(lastRental == null)
            {
                rentalNo = "RN00001";
            } else
            {
                rentalNo = "RN" + (Convert.ToInt32(lastRental.RentalNo.Substring(3, lastRental.RentalNo.Length - 3)) + 1).ToString("D5");
            }
            
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                 rental = new Rental
                {
                    RentalNo = rentalNo,
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);

            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
