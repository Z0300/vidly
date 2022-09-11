using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.App.Models;
using Vidly.App.ViewModels;

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
            var rental = _context.RentalHeaders
                .Include(x => x.Customer)
                .FirstOrDefault(c => c.CustomerId == id);

            var rentedMovies = _context.RentalHeaders
                .Where(x => x.CustomerId == id)
                .Select(x => x.MovieId)
                .AsQueryable();

            var rentalDetails = _context.RentalDetails
                .Single(x => x.RentalNo == rental.RentalNo);

            var viewModel = new RentalFormViewModel
            {
                RentalNo = rental.RentalNo,
                Customer = _context.Customers.Single(c => c.Id == rental.CustomerId),
            };

            return View("RentedList", viewModel);
        }

        public ActionResult History(int id)
        {
            var customer = _context.Customers.Single(x => x.Id == id);
            var vm = new CustomerViewModel
            {
                Customer = customer
            };

            return View("RentalHistory", vm);

        }

    }
}