using Microsoft.AspNetCore.Mvc;

namespace ShortenUrlApp.Controllers
{
    public class BaseController : Controller
    {
        public virtual IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
