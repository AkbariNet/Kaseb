using KasebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KasebAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Ad> Ads { get; set; }

        public DbSet<AdImage> AdImages { get; set; }

    }
}