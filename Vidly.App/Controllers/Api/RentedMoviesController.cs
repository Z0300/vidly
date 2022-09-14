using System.Linq;
using System.Web.Http;
using Vidly.App.Models;
using System.Data.Entity;

namespace Vidly.App.Controllers.Api
{
    public class RentedMoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public RentedMoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetRentedMovies(int id, string rentalNo)
        {
            if (id == null)
                return BadRequest("Customer not found.");


            var movies = _context.RentalHeaders
               .Include(x => x.Movie)
               .Include(x => x.Movie.Genre)
               .Where(x => x.CustomerId == id && x.IsReturn == false && x.RentalNo == rentalNo)
               .AsQueryable();


            return Ok(movies);
        }
    }
}
