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

        public ActionResult Details(int id, string name)
        {
            var rental = _context.RentalHeaders
                .Include(x => x.Customer)
                .Where(c => c.CustomerId == id && c.RentalNo == name)
                .FirstOrDefault();

            var viewModel = new RentalFormViewModel
            {
                RentalNo = rental.RentalNo,
                Customer = rental.Customer,
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