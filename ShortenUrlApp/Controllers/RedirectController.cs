using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShortenUrlApp.Data;

namespace ShortenUrlApp.Controllers
{
    public class RedirectController : BaseController
    {
        private readonly ShortenUrlAppContext _context;

        public RedirectController(ShortenUrlAppContext context)
        {
            _context = context;
        }

        [Route("/s")]
        public IActionResult RedirectToIndex()
        {            
            return RedirectToAction("Index");
        }

        [Route("/s/{*catchAll}")]
        public IActionResult RedirectToLongUrl(string catchAll)
        {
            string shortUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            var longUrl = _context.ShortenUrl?.Where(e => e.ShortUrl == shortUrl).Select(e => e.LongUrl).FirstOrDefault();

            if (longUrl.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }

            return Redirect(longUrl);
        }
    }
}
