using System;
using System.ComponentModel.DataAnnotations;

namespace ShortenUrlApp.Attributes
{
    public class ValidLinkAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string link = value.ToString();

            if (!Uri.TryCreate(link, UriKind.Absolute, out Uri result) ||
                (result.Scheme != Uri.UriSchemeHttp && result.Scheme != Uri.UriSchemeHttps))
            {
                return new ValidationResult("Invalid link format.");
            }

            return ValidationResult.Success;
        }
    }
}

