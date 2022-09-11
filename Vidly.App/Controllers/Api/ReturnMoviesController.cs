using System;
using System.Linq;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;

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
        public void CreateReturnMovies(ReturnMovieDto movieDto)
        {
            var movies = _context.Movies.Where(
                m => movieDto.MovieIds.Contains(m.Id)).AsQueryable();

            foreach (var movie in movies)
            {
                movie.NumberAvailable++;
            }

            var rentals = _context.RentalHeaders
                .Where(x => x.RentalNo == movieDto.RentalNo)
                .AsQueryable();

            foreach (var rental in rentals)
            {
                rental.IsReturn = true;
                rental.DateReturned = DateTime.Now;
            }

            var rentalDetails = _context.RentalDetails.Where(x => x.RentalNo == movieDto.RentalNo).First();
            _context.RentalDetails.Remove(rentalDetails);

            _context.SaveChanges();
        }
    }
}
