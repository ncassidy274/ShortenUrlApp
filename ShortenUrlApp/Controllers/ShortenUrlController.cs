using ShortenUrlApp.Models;
using Microsoft.AspNetCore.Mvc;
using ShortenUrlApp.Data;
using Microsoft.IdentityModel.Tokens;

namespace ShortenUrlApp.Controllers
{
    public class ShortenUrlController : BaseController
    {
        private readonly ShortenUrlAppContext _context;

        public ShortenUrlController(ShortenUrlAppContext context)
        {
            _context = context;
        }

        #region Actions      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete([Bind("LongUrl")] ShortenUrl model)
        {
            {
                //Inputted url is invalid - try again 
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Error", "Home", model);
                }

                //Inputted url is Valid - create shortUrl and save to DB.
                _context.Add(model);
                HttpContext httpContext = HttpContext;
                GetShortUrl(model, httpContext);
                await _context.SaveChangesAsync();                
            }
            return View(model);
        }

        //Incase of url hacking, redirect to home.
        public IActionResult Complete()
        {
            return RedirectToAction("Index");
        }

        #endregion

        #region Helpers

        protected void GetShortUrl(ShortenUrl model, HttpContext httpContext)
        {
            model.ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/s/{GenerateUniqueString()}";
        }

        private string GenerateUniqueString()
        {
            var randomString = GenerateRandomString();
            while (ShortUrlExists(randomString))
            {
                randomString = GenerateRandomString();
            }
            return randomString;
        }

        private string GenerateRandomString()
        {
            Random random = new Random();         

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var generatedString = new string(Enumerable.Range(1, 5).Select(m => chars[random.Next(chars.Length)]).ToArray());

            return generatedString;
        }

        private bool ShortUrlExists(string value)
        {
            bool? checkExists = _context.ShortenUrl?.Any(e => e.ShortUrl == value);

            bool exists = checkExists ?? false;

            return exists;
        }

        #endregion
    }
}
