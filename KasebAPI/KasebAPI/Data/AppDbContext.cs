using KasebAPI.Models;
using KasebAPI.Models.OTP;
using KasebAPI.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static KasebAPI.Models.OTP.OTPRequest;
using static KasebAPI.Models.Profile.ProfileModel;

namespace KasebAPI.Data
{
    public class AppDbContext : IdentityDbContext<ProfileModel>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<VerifyOTPRequest> OtpStorage { get; set; }

        public DbSet<Ad> Ads { get; set; }

        public DbSet<AdImage> AdImages { get; set; }
        public DbSet<ProfileModel> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
}
}