using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.App.Models;

namespace Vidly.App.Controllers.Api
{
    public class RentalHistoriesController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalHistoriesController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetRentalHistories(int id)
        {
            if (id == null)
                return BadRequest("Customer not found.");

            var moviesRented = _context.RentalHeaders
               .Include(x => x.Movie)
               .Include(x =>x.Movie.Genre)
               .Where(x => x.CustomerId == id)
               .AsQueryable();

            return Ok(moviesRented);
        }
    }
}
