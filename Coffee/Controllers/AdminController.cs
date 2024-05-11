using Coffee.Models.Entity;
using Coffee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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

        public async Task<ActionResult> CreateNews()
        {
            return View();
        }
    }
}
