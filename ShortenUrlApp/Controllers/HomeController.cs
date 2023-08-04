using Microsoft.AspNetCore.Mvc;
using ShortenUrlApp.Models;
using System.Diagnostics;

namespace ShortenUrlApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(ShortenUrl model)
        {
            return View("Index", model);
        }

    }
}