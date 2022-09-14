using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;
using System.Data.Entity;

namespace Vidly.App.Controllers.Api
{
    public class ReturnMoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public ReturnMoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateReturnMovies(ReturnMovieDto movieDto)
        {
            if (movieDto.MovieIds == null)
                return BadRequest("Please select at least one movie.");

            var movies = _context.Movies
                .Where(m => movieDto.MovieIds.Contains(m.Id))
                .AsQueryable();

            foreach (var movie in movies)
            {
                movie.NumberAvailable++;
            }

            _context.SaveChanges();

            var rentals = _context.RentalHeaders
                .Include(x => x.Movie)
                .Where(x => x.RentalNo == movieDto.RentalNo)
                .AsQueryable();

            foreach (var rental in rentals)
            {
                rental.IsReturn = true;
                rental.DateReturned = DateTime.Now;
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateReturnMovie(ReturnSingleMovieDto returnDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model must be vallid");

            var movie = _context.Movies.Single(x => x.Id == returnDto.MovieId);
            movie.NumberAvailable++;

            var rentalInDb = _context.RentalHeaders
                .SingleOrDefault(c => c.MovieId == returnDto.MovieId && c.CustomerId == returnDto.CustomerId && c.RentalNo == returnDto.RentalNo);

            if (rentalInDb == null)
                return NotFound();

            rentalInDb.IsReturn = true;
            rentalInDb.DateReturned = DateTime.Now;
            _context.SaveChanges();

            return Ok();
        }

    }
}
