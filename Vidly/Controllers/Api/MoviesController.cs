using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;


namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/movies

 

        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies.Include(m => m.Genre).Where(m => m.NumberAvailable > 0);
            if(!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m=>m.Name.Contains(query));
            }

            return moviesQuery.ToList().Select(Mapper.Map<Movie,MovieDto>);
        }


    
        //public IHttpActionResult GetMovie()
        //{
        //   // return _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDto>);
        //    var movieDtos = _context.Movies.
        //        Include(m => m.Genre)   
        //        .ToList()
        //        .Select(Mapper.Map<Movie,MovieDto>);
        //    return Ok(movieDtos);
        //}

        //Get api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie,MovieDto>(movie));

        }

        // Post api/movis
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto,Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }
        

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id,MovieDto movieDto)
       // public void Updatemovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                //       throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    
        //public void DeleteMovie(int id)
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
