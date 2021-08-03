using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieModels.SearchResults;
using MovieWebsite.CommandHandlers;
using MovieWebsite.Models;
using System.Diagnostics;
using System.Linq;

namespace MovieWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IFindMoviesCommandHandlers _findMoviesCommandHandlers;
        private readonly IGetYearsCommandHandler _getYearsCommandHandler;
        private readonly IGetGenresCommandHandler _getGenresCommandHandler;

        public HomeController(
            ILogger<HomeController> logger,
            IMapper mapper,
            IFindMoviesCommandHandlers findMoviesCommandHandlers,
            IGetYearsCommandHandler getYearsCommandHandler,
            IGetGenresCommandHandler getGenresCommandHandler
        )
        {
            _logger = logger;
            _mapper = mapper;
            _findMoviesCommandHandlers = findMoviesCommandHandlers;
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
            if (string.IsNullOrWhiteSpace(search))
                return BadRequest("search is required");

            if(genres != null && genres.Any(genre => string.IsNullOrWhiteSpace(genre)))
                return BadRequest("one or more genre is not valid");

            if(years != null && years.Any(year => year < 1900))
                return BadRequest("one or more year is not valid");

            var result = _findMoviesCommandHandlers.Handle(search, genres, years);

            return Json(_mapper.Map<MovieSearchResult[]>(result));
        }
    }
}
