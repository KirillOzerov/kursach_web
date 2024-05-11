using Coffee.Models.Entity;
using Coffee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Coffee.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private NewsRepository _newsRepository;

        public AdminController(NewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            var ListUsers = new List<string>();

            return View(ListUsers);
        }

        public async Task<ActionResult> News()
        {
            var listNews = await _newsRepository.GetNewsAsync();

            return View(listNews);
        }

        [HttpGet]
        public async Task<ActionResult> CreateNews()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNews(News news)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                news.AuthorId = userId;

                news.Date = DateTime.SpecifyKind(news.Date, DateTimeKind.Utc);

                var result = await _newsRepository.CreateNewsAsync(news);
            }
            return Redirect("/Admin/News");
        }
    }
} 
