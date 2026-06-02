using KasebAPI.Models;
using KasebAPI.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static KasebAPI.Models.Profile.ProfileModel;

namespace KasebAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Ad> Ads { get; set; }

        public DbSet<AdImage> AdImages { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }

        }
}