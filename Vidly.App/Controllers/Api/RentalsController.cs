using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;

namespace Vidly.App.Controllers.Api
{
    [RoutePrefix("api")]
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/rented/movies
        // /pagination
        [Route("rentals")]
        public IHttpActionResult GetRentals(string query = null)
        {
            var rentalsQuery = _context.RentalHeaders
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
                .OrderByDescending(a => a.RentalNo)
                .Skip(skip)
                .Take(limit > 0 ? limit : pageSize);


            var rentalsDto = rentalsQuery
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

        [Route("returns")]
        public IHttpActionResult GetReturnedMovies(int id)
        {
            if (id == null)
                return BadRequest("Customer not found.");

            var movies = _context.RentalDetails
                  .Include(x => x.Movie)
                  .Include(x => x.Movie.Genre)
                  .Where(x => x.RentalHeaderId == id && x.IsReturn == true)
                  .AsQueryable();

            return Ok(movies);
        }

        [Route("rented")]
        public IHttpActionResult GetRentedMovies(int id)
        {
            if (id == null)
                return BadRequest("Details not found.");

            var movies = _context.RentalDetails
                 .Include(x => x.Movie)
                 .Include(x => x.Movie.Genre)
                 .Where(x => x.RentalHeaderId == id && x.IsReturn == false)
                 .AsQueryable();

            return Ok(movies);
        }

        [HttpPost]
        [Route("newRentals")]
        public IHttpActionResult CreateNewRetals(NewRentalDto newRental)
        {
            var rental = new RentalHeader();

            var lastRental = _context.RentalHeaders
                .OrderByDescending(c => c.Id)
                .FirstOrDefault();

            string rentalNo;

            if (lastRental == null)
            {
                rentalNo = "RN0001";
            }
            else
            {
                rentalNo = "RN" + (Convert.ToInt32(lastRental.RentalNo.Substring(3, lastRental.RentalNo.Length - 3)) + 1).ToString("D4");
            }

            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).AsQueryable();

            rental = new RentalHeader
            {
                RentalNo = rentalNo,
                Customer = customer
            };

            _context.RentalHeaders.Add(rental);

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                var rentalDetails = new RentalDetail
                {
                    RentalHeader = rental,
                    Movie = movie,
                    DateRented = DateTime.Now,
                    IsReturn = false
                };

                _context.RentalDetails.Add(rentalDetails);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("returnMovies")]
        public IHttpActionResult CreateReturnMovies(ReturnMoviesDto rentalDto)
        {
            if (rentalDto.MovieIds == null)
                return BadRequest("Please select at least one movie.");

            var movies = _context.Movies
                .Where(m => rentalDto.MovieIds.Contains(m.Id))
                .AsQueryable();

            var rentalDetails = _context.RentalDetails
                .Where(x => x.RentalHeaderId == rentalDto.rentalId)
                .AsQueryable();

            foreach (var movie in movies)
                movie.NumberAvailable++;

            foreach(var rentalDetail in rentalDetails)
            {
                rentalDetail.DateReturned = DateTime.Now;
                rentalDetail.IsReturn = true;
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("returnMovie")]
        public IHttpActionResult UpdateReturnMovie(ReturnMovieDto returnDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model must be vallid");

            var movie = _context.Movies.Single(x => x.Id == returnDto.rentalId);
            movie.NumberAvailable++;

            var rentalInDb = _context.RentalDetails
                .SingleOrDefault(c => c.MovieId == returnDto.MovieId && c.RentalHeaderId == returnDto.rentalId);

            if (rentalInDb == null)
                return NotFound();

            rentalInDb.IsReturn = true;
            rentalInDb.DateReturned = DateTime.Now;
            _context.SaveChanges();

            return Ok();
        }

    }
}
