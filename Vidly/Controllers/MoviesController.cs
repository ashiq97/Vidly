using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MoviesController : Controller
	{
		//
		// GET: /Movies/Random
		private ApplicationDbContext _context;
		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

        public ViewResult Index()
        {
            //var movies = GetMovies();


            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            //// var movies = _context.Movies.ToList();


            //return View(movies);
            if (User.IsInRole(RoleName.CanMangeMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        [Authorize(Roles=RoleName.CanMangeMovies)]
		public ActionResult MovieForm()
		{
			var genre = _context.Genres.ToList();

			var viewModel = new MovieFormViewModel
			{
				Movie = new Movie(),
				Genres = genre
			};
			return View("MovieForm", viewModel);
		}

		// for adding 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
		   if(!ModelState.IsValid)
		   {
			   var viewModel = new MovieFormViewModel
			   {
				   Movie = movie,
				   Genres = _context.Genres.ToList()

			   };
			   return View("MovieForm", viewModel);
		   }

			if (movie.Id == 0) // it is a new Movie
			{
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);

			}
			else // it is existing movie
			{
				var moiveDb = _context.Movies.Single(m => m.Id == movie.Id);
				moiveDb.Name = movie.Name;
				moiveDb.ReleaseDate = movie.ReleaseDate;
				moiveDb.GenreId = movie.GenreId;
				// moiveDb.Genre = movie.Genre;
				moiveDb.NumberInStock = movie.NumberInStock;

			}

			_context.SaveChanges();
			return RedirectToAction("Index", "Movies");

		}


		public ActionResult Edit(int id)
		{

			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
			if (movie == null)
				return HttpNotFound();
			var viewModel = new MovieFormViewModel
			{
				Movie = movie,
				Genres = _context.Genres.ToList()
			};

			return View("MovieForm", viewModel);
		}


		

		public ActionResult Details(int id)
		{
			var movies = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

			if (movies == null)
				return HttpNotFound();
			return View(movies);
		}
		//private IEnumerable<Movie> GetMovies()
		//{
		//    return new List<Movie>
		//    {
		//        new Movie { Id = 1, Name = "Shrek" },
		//        new Movie { Id = 2, Name = "Wall-e" }
		//    };
		//}



		public ActionResult Random()
		{

			var movie = new Movie() { Name = "Shrek!" };
			// creating new instance and adding a value
			var customers = new List<Customer>
			{
				new Customer {Name = "Customer 1"},
				new Customer {Name ="Customer 2"}
			};

			var viewModel = new RandomMovieViewModel
			{
				Movie = movie,
				Customers = customers
			};

			return View(viewModel);
			//return View(movie);
			//return Content("Hello World");
			//return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
		}

		/*public ActionResult Edit(int id)
		{
			return Content("Id : " + id);
		}
		public ActionResult Index(int? pageIndex,string sortBy)
		{
			if (!pageIndex.HasValue)
				pageIndex = 1;
			if (String.IsNullOrWhiteSpace(sortBy))
				sortBy = "name";
			return Content(String.Format("PageIndex = {0}&SortBy = {1}", pageIndex, sortBy));
		}

		[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
		public ActionResult ByReleaseDate(int year,int month)
		{
			
			return Content(year + "/"+ month);
		}
		*/

	}
}