using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortenUrlApp.Models;

namespace ShortenUrlApp.Data
{
    public class ShortenUrlAppContext : DbContext
    {
        public ShortenUrlAppContext (DbContextOptions<ShortenUrlAppContext> options)
            : base(options)
        {
        }

        public DbSet<ShortenUrlApp.Models.ShortenUrl> ShortenUrl { get; set; } = default!;
    }
}
