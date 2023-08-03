using Microsoft.AspNetCore.Mvc;
using ShortenUrlApp.Data;

namespace ShortenUrlApp.Controllers
{
    public class RedirectController : Controller
    {
        private readonly ShortenUrlAppContext _context;

        public RedirectController(ShortenUrlAppContext context)
        {
            _context = context;
        }

        [Route("/s/{*catchAll}")]
        public IActionResult RedirectToLongUrl()
        {
            string shortUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            var longUrl = _context.ShortenUrl?.Where(e => e.ShortUrl == shortUrl).Select(e => e.LongUrl).FirstOrDefault();
            //ToDo: Add check for null
            return Redirect(longUrl);
        }
    }
}
