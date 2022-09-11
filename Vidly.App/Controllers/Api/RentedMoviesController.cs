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

        public IHttpActionResult GetRentedMovies(int id)
        {
            var allMoviesRented = _context.RentalHeaders
                .Where(x => x.CustomerId == id)
                .Select(x => x.MovieId)
                .AsQueryable();

            var movies = _context.Movies.Include(x => x.Genre).Where(
                m => allMoviesRented.Contains(m.Id))
                .AsQueryable();

            return Ok(movies);
        }
    }
}
