using System.Web.Mvc;
using Vidly.App.Models;
using System.Data.Entity;
using Vidly.App.ViewModels;
using System.Linq;

namespace Vidly.App.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var rental = _context.Rentals
                .Include(x => x.Customer)
                .FirstOrDefault(c => c.CustomerId == id);

            var rentedMovies = _context.Rentals
                .Where(x => x.CustomerId == id)
                .Select(x => x.MovieId)
                .ToList();

            var viewModel = new RentalFormViewModel
            {
                Customer = _context.Customers.Single(c => c.Id == rental.CustomerId),
                Movies = _context.Movies
                            .Include(x => x.Genre)
                            .Where(x => rentedMovies.Contains(x.Id))
            };

            return View(viewModel);
        }
    }
}