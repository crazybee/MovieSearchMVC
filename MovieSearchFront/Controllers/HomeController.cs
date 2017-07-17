using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MovieSearchFront.Models;

namespace MovieSearchFront.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index(string movieName)
        {
            if (string.IsNullOrEmpty(movieName))
            {
                return View();
            }
            var movieList = new List<MovieModel>();
            using (var service = new Helpers.MovieSearchHelper(movieName))
            {
                movieList.AddRange(await service.GetMoviesAsync());
            }
            return View(movieList);
           
        }
    }
}
