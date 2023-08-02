using ShortenUrlApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ShortenUrlApp.Controllers
{
    public class ShortenUrlController : Controller
    {
        #region Actions

        //ToDo: Stop being able to jump to here
        //ToDo: Add validation on input
        //ToDo: Add styling to Form box on view
        [HttpPost]
        public ActionResult Complete(ShortenUrl model)
        {
            //ToDo: Check if longurl already exists in db

            HttpContext context = HttpContext;
            model.ShortUrl = BuildShortUrl(context);
            //save model to db

            return View("Complete", model);
        }

        #endregion

        #region Helpers

        protected string BuildShortUrl(HttpContext context)
        {
            var generatedString = GenerateUniqueString();                     

            var responseUri = $"{context.Request.Scheme}://{context.Request.Host}/{generatedString}";

            return responseUri;
        }

        private string GenerateUniqueString()
        {
            Random random = new Random();

            //ToDo: Check caps/lowercase
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var generatedString = new string(Enumerable.Range(1, 5).Select(m => chars[random.Next(chars.Length)]).ToArray());

            //check shortUrl does not already exist

            return generatedString;
        }

        #endregion
    }
}
