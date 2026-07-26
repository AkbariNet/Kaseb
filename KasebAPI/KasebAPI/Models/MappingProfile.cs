
using AutoMapper;
using KasebAPI.Models.DTOs;
using KasebAPI.Models.Profile;
using System.Net;

namespace KasebAPI.Models
{
    public class MappingProfile : AutoMapper.Profile
    {

        public MappingProfile()
        {
            CreateMap<ProfileModel, ProfileModelDTO.UserProfileDTO>();
            CreateMap<ProfileModelDTO.UserProfileDTO, ProfileModel>(); // نقشه برعکس

        }
    }
}




