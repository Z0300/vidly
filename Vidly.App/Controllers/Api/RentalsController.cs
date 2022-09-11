using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;

namespace Vidly.App.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/rented/movies
        // /pagination
        public IHttpActionResult GetRentals(string query = null)
        {
            var rentalsQuery = _context.RentalHeaders
                .Include(m => m.Movie)
                .Include(c => c.Customer);

            var Request = HttpContext.Current.Request.Params;
            int totalRecord = rentalsQuery.Count();
            int filterRecord = 0;

            var draw = Request["draw"];
            var limit = Convert.ToInt32(Request["limit"] ?? "0");
            var requestSearchValue = Request["search[value]"] ?? "";
            var extJSSearchValue = Request["filter"] ?? "";
            int pageSize = Convert.ToInt32(Request["length"] ?? "10");

            if (requestSearchValue != "")
                query = requestSearchValue;

            int skip = Convert.ToInt32(Request["start"] ?? "0");


            if (!String.IsNullOrWhiteSpace(query))
                rentalsQuery = rentalsQuery
                    .Where(a => a.RentalNo.Contains(query));
            filterRecord = rentalsQuery.Count();


            //pagination
            //if (skip > 0)
            rentalsQuery = rentalsQuery
                .OrderBy(a => a.RentalNo)
                .Skip(skip)
                .Take(limit > 0 ? limit : pageSize);


            var rentalsDto = rentalsQuery
                .GroupBy(x => new { x.RentalNo, x.Customer, x.IsReturn })
                .Select(x => new { x.Key.RentalNo, x.Key.Customer, x.Key.IsReturn })
                .AsQueryable();


            var obj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = rentalsDto
            };

            return Ok(obj);
        }


    }
}
