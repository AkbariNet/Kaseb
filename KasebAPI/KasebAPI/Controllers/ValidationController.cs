using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;
using KasebAPI.Models.OTP;

namespace KasebAPI.Controllers
{
    [ApiController]
    [Route("Profile")]
    public partial class ProfileController : ControllerBase
    {
        private readonly TimeSpan _otpExpirationTime = TimeSpan.FromMinutes(5);
        private readonly Dictionary<string, (string Code, DateTime Expiration)> _otpStorage = new Dictionary<string, (string Code, DateTime Expiration)>();

        [HttpGet("SendOTP")]
        public async Task SendVerifyAsync(string PhoneNumber)
        {
            //phone number like 9180000000
            SmsIr smsIr = new SmsIr("o5vZxQn9YEK0x4amZIdhGTFPNgh2lDohk6m8CxjQTQMaXFlZ");

            int templateId = 335888;
            VerifySendParameter[] verifySendParameters = {
                new VerifySendParameter("NAME", "User Name"),
                new VerifySendParameter("CODE", Random.Shared.Next(00000, 99999).ToString())
            };

            var response = await smsIr.VerifySendAsync(PhoneNumber, templateId, verifySendParameters);

            VerifySendResult sendResult = response.Data;
            int messageId = sendResult.MessageId;
            decimal cost = sendResult.Cost;

            // ذخیره کد OTP و زمان انقضا
            string phoneNumber = PhoneNumber; // شماره تلفن کاربر
            string code = verifySendParameters[1].Value;
            DateTime expiration = DateTime.Now + _otpExpirationTime;
            _otpStorage[phoneNumber] = (code, expiration);
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPRequest.VerifyOTPRequest request)
        {
            string phoneNumber = request.PhoneNumber;
            string userCode = request.UserCode;

            if (!_otpStorage.ContainsKey(phoneNumber))
            {
                return BadRequest("Invalid or Expired OTP");
            }

            var otpInfo = _otpStorage[phoneNumber];
            string storedCode = otpInfo.Code;
            DateTime expiration = otpInfo.Expiration;

            if (DateTime.Now > expiration)
            {
                _otpStorage.Remove(phoneNumber); // پاک کردن کد منقضی شده
                return BadRequest("Invalid or Expired OTP");
            }

            if (userCode == storedCode)
            {
                _otpStorage.Remove(phoneNumber); // پاک کردن کد پس از استفاده
                return Ok(new { Message = "OTP Verified Successfully" });
            }
            else
            {
                return BadRequest("Invalid OTP");
            }
        }
    }

}
