using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieWebsite.CommandHandlers;
using MovieWebsite.Models;
using System.Diagnostics;

namespace MovieWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetYearsCommandHandler _getYearsCommandHandler;
        private readonly IGetGenresCommandHandler _getGenresCommandHandler;

        public HomeController(
            ILogger<HomeController> logger,
            IGetYearsCommandHandler getYearsCommandHandler,
            IGetGenresCommandHandler getGenresCommandHandler
        )
        {
            _logger = logger;
            _getYearsCommandHandler = getYearsCommandHandler;
            _getGenresCommandHandler = getGenresCommandHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            var model = new SearchViewModel();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetYears()
        {
            return Json(_getYearsCommandHandler.Handle());
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            return Json(_getGenresCommandHandler.Handle());
        }

        [HttpPost]
        public IActionResult FindMovies(string search, string[] genres, int[] years)
        {
            return Json(new { hi = "there" });
        }
    }
}
