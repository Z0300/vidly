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

            var viewModel = new RentalDetailsViewModel
            {
                RentalId = id,
                Customer = _context.RentalHeaders.Include(c => c.Customer).Single(x => x.Id == id).Customer
            };

            return View("RentalDetails", viewModel);
        }

       

    }
}