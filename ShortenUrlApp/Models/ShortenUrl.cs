using ShortenUrlApp.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShortenUrlApp.Models;

public class ShortenUrl
{
    #region Properties

    public int Id { get; set; }

    [Required(ErrorMessage = "No URL Found.")]
    [ValidLink]
    public string LongUrl { get; set; }

    public string? ShortUrl { get; set; }

    #endregion

}