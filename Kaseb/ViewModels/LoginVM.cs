
using Kaseb.Services.ShowingContext;
using Kaseb.Views;
using KasebCore.Models.Element;
using KasebCore.Models.OTPRequest;
using KasebCore.Models.Profile;
using KasebCore.Models.Search;
using KasebCore.Models.Services.AdService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using Profile = KasebCore.Models.Profile.Profile;

namespace Kaseb.ViewModels
{
    public static class LoginVM
    {
        public static string PhoneNumber { get; set; } = string.Empty;
        public static string Validation { get; set; } = string.Empty;

        public static async Task<GetResoultInfo> SendOTP()
        {
            var response = await AdServiceModel.httpClient
                .GetAsync($"Profile/SendOTP?PhoneNumber={PhoneNumber}"
                );

            if (response.IsSuccessStatusCode)
            {

                Login.OTPSent.Invoke(Login.LoginPageStatement.GetCodePage);

                return new GetResoultInfo()
                {
                    StatusCode = 1 ,
                    IsSuccess = true ,
                    Message = string.Empty
                };
            }
            else
            {

                 return new GetResoultInfo()
                {
                    StatusCode = 0,
                    IsSuccess = false,
                    Message = string.Empty
                }; 
            }

        }
        public static async Task<GetResoultInfo> SendValidation ()
        {
            OTPRequest.VerifyOTPRequestModel.PhoneNumber = PhoneNumber;
            OTPRequest.VerifyOTPRequestModel.UserCode = Validation;
            var form = new MultipartFormDataContent();

            var properties = OTPRequest.VerifyOTPRequestModel.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(OTPRequest.VerifyOTPRequestModel);

                if (value == null)
                    continue;

                form.Add(
                    new StringContent(value.ToString()),
                    prop.Name
                );
            }
            var response = await AdServiceModel.httpClient
                .PostAsync("Profile/VerifyOTP", form
                );

            if (!response.IsSuccessStatusCode)
            {

                return new GetResoultInfo()
                {
                    StatusCode = 0,
                    IsSuccess = false,
                    Message = string.Empty
                };

            }
            else
            {
               var Model = await ResponseToModel(response);
                if (Model != null)
                {

                    Profile.MainProfile = Model;
                    return new GetResoultInfo()
                    {
                        StatusCode = 1,
                        IsSuccess = true,
                        Message = string.Empty
                    };
                }
                else
                {
                    return new GetResoultInfo()
                    {
                        StatusCode = 0,
                        IsSuccess = false,
                        Message = string.Empty
                    };

                }

            }

        }
        private static async Task<ProfileModel> ResponseToModel(HttpResponseMessage response)
        {

            var resp = await response.Content.ReadAsStringAsync();
            var list= JsonSerializer.Deserialize<ProfileModel>(resp);
            return list;
        }
    }
}
