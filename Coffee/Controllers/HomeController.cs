using Coffee.Models;
using Coffee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coffee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private NewsRepository _newsRepository;

        public HomeController(ILogger<HomeController> logger, NewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        public async Task<ActionResult> Index()
        {
            var listNews = await _newsRepository.GetOnlyActiveNewsAsync();
            bool isAdmin = User.IsInRole("Administrator");
            ViewBag.IsAdmin = isAdmin;
            return View(listNews);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public async Task<ActionResult> News()
        {
            var listNews = await _newsRepository.GetOnlyActiveNewsAsync();

            return View(listNews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
