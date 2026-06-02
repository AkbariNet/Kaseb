using Azure.Core;
using KasebAPI.Interfaces;
using KasebAPI.Models;
using KasebAPI.Models.DTOs;
using KasebAPI.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using static KasebAPI.Models.DTOs.ProfileModelDTO;
using static KasebAPI.Models.Profile.ProfileModel;

namespace KasebAPI.Controllers
{
    public partial class Controller : ControllerBase
    {
        [HttpPost("create-profileTemp")]

        public async Task<IActionResult> CreateProfile([FromForm] UserProfileDTO model)

        {

            UserProfile userProfile = new UserProfile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                RegistrationTime = model.RegistrationTime,
                City = model.City,
                MobileNumber = model.MobileNumber,  // ! **امنیت: رمزنگاری شماره موبایل ضروری است!**
                IsVerified = model.IsVerified,
                NationalId = model.NationalId, // ! **امنیت: رمزنگاری شماره ملی ضروری است!**
                ProfilePictureUrl = model.ProfilePictureUrl,
                Role = model.Role,
                Description = model.Description,
                Location = model.Location,
                LastVisitTime = DateTime.Now,
            };

            context.Profiles.Add(userProfile);

            await context.SaveChangesAsync();




            return Ok(userProfile);

        }


    }
}
