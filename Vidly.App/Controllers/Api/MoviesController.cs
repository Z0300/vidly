using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;
using System.Data.Entity;
using System.Web;

namespace Vidly.App.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies.Include(a => a.Genre);

            var Request = HttpContext.Current.Request.Params;
            int totalRecord = moviesQuery.Count();
            int filterRecord = 0;

            var draw = Request["draw"];
            var limit = Convert.ToInt32(Request["limit"] ?? "0");
            var requestSearchValue = Request["search[value]"] ?? "";
            var extJSSearchValue = Request["filter"] ?? "";
            int pageSize = Convert.ToInt32(Request["length"] ?? "10");

            if (requestSearchValue != "")
                query = requestSearchValue;

            int skip = Convert.ToInt32(Request["start"] ?? "0");


            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(a => a.Name.Contains(query));
            filterRecord = moviesQuery.Count();

            //pagination
            //if (skip > 0)
            moviesQuery = moviesQuery
                .OrderBy(a => a.Name)
                .Skip(skip)
                .Take(limit > 0 ? limit : pageSize);


            var mmoviesDto = moviesQuery
                 .ToList();

            var obj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = mmoviesDto
            };

            return Ok(obj);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }


        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDto, customerInDb);

            _context.SaveChanges();

        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }

    }
}
