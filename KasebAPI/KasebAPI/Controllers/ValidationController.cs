using AutoMapper;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;
using KasebAPI.Data;
using KasebAPI.Models.DTOs;
using KasebAPI.Models.OTP;
using KasebAPI.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static KasebAPI.Models.OTP.OTPRequest;

namespace KasebAPI.Controllers
{
    [ApiController]
    [Route("Profile")]
    public partial class ProfileController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly TimeSpan _otpExpirationTime = TimeSpan.FromMinutes(5);
        private readonly IWebHostEnvironment env;


        public ProfileController(AppDbContext context, IWebHostEnvironment env,
    UserManager<ProfileModel> userManager,
    IMapper mapper)
        {

            this.context = context;

            this.env = env;

            _userManager = userManager;

            _mapper = mapper;

        }
        [HttpGet("SendOTP")]
        public async Task<IActionResult> SendVerifyAsync(string PhoneNumber)
        {
            try
            {
                //phone number like 9180000000
                SmsIr smsIr = new SmsIr("ig5wIp5M20ahKzmhg1TVeLcHgs7gLa2RxInN9p6HyUJq2og4");

                int templateId = 335888;
                VerifySendParameter[] verifySendParameters = {
                new VerifySendParameter("NAME", "User Name"),
                new VerifySendParameter("CODE", /*Random.Shared.Next(10000, 99999).ToString()*/ "12345")
            };

                var response = await smsIr.VerifySendAsync(PhoneNumber, templateId, verifySendParameters);

                VerifySendResult sendResult = response.Data;
                int messageId = sendResult.MessageId;
                decimal cost = sendResult.Cost;

                // ذخیره کد OTP و زمان انقضا
                context.OtpStorage.Add(new VerifyOTPRequest()
                {
                    PhoneNumber = PhoneNumber,
                    UserCode = verifySendParameters[1].Value,
                    Expiration = DateTime.Now + _otpExpirationTime

                });

                await context.SaveChangesAsync();
                if (response.Status == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(response.Data);
                }

            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }

        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromForm] OTPRequest.VerifyOTPRequest request)
        {
            string phoneNumber = request.PhoneNumber;
            string userCode = request.UserCode;

            bool IsValid = false;
            VerifyOTPRequest verifyOTPRequest = new VerifyOTPRequest();
            foreach (VerifyOTPRequest item in context.OtpStorage)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    IsValid = true;
                    verifyOTPRequest = item;
                }
            }
            if (!IsValid)
            {
                return BadRequest("Invalid or Expired OTP");
            }


            string storedCode = verifyOTPRequest.UserCode;
            DateTime expiration = verifyOTPRequest.Expiration;

            if (DateTime.Now > expiration)
            {
                context.OtpStorage.Remove(verifyOTPRequest); // پاک کردن کد منقضی شده
                await context.SaveChangesAsync();
                return BadRequest("Invalid or Expired OTP");
            }

            if (userCode == storedCode)
            {
                context.OtpStorage.Remove(verifyOTPRequest);  // پاک کردن کد پس از استفاده
                await context.SaveChangesAsync();
                var userDto = await LoginUser(phoneNumber);
                return Ok(userDto);
            }
            else
            {
                return BadRequest("Invalid OTP");
            }
        }

        private readonly UserManager<ProfileModel> _userManager;
        private readonly IMapper _mapper;

        private async Task<ProfileModel> LoginUser(string phoneNumber)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);

            if (user == null)
            {
                var newUser = _mapper.Map<ProfileModel>(
                    new ProfileModel
                    {
                        UserName = phoneNumber,
                        MobileNumber = phoneNumber,
                        RegistrationTime = DateTime.Now,
                        Id = phoneNumber
                    });

                var result = await _userManager.CreateAsync(newUser);

                user = newUser;
                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(",", result.Errors.Select(e => e.Description)));
                }

            }

            return _mapper.Map<ProfileModel>(user);
        }
    }
}
