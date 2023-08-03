using ShortenUrlApp.Models;
using Microsoft.AspNetCore.Mvc;
using ShortenUrlApp.Data;

namespace ShortenUrlApp.Controllers
{
    public class ShortenUrlController : Controller
    {
        private readonly ShortenUrlAppContext _context;

        public ShortenUrlController(ShortenUrlAppContext context)
        {
            _context = context;
        }

        #region Actions        

        //ToDo: Stop being able to jump to here
        //ToDo: Add validation on input
        //ToDo: Add styling to Form box on view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete([Bind("Id,LongUrl")] ShortenUrl model)
        {            
            {
                _context.Add(model);

                HttpContext httpContext = HttpContext;

                GetShortUrl(model, httpContext);

                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        #endregion

        #region Helpers

        protected void GetShortUrl(ShortenUrl model, HttpContext httpContext)
        {
            if (LongUrlExists(model.LongUrl))
            {
                //return stored short url
            }
            else
            {
                model.ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/s/{GenerateUniqueString()}";
            }            
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

            //ToDo: Check caps/lowercase
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var generatedString = new string(Enumerable.Range(1, 5).Select(m => chars[random.Next(chars.Length)]).ToArray());

            return generatedString;
        }

        private bool LongUrlExists(string value)
        {
            return false;
        }

        private bool ShortUrlExists(string value)
        {
            return false;
        }

        #endregion
    }
}
