using System;
using System.Collections.Generic;
using System.Text;

namespace KasebCore.Models.OTPRequest
{
    public static class OTPRequest
    {
        public static VerifyOTPRequest VerifyOTPRequestModel= new VerifyOTPRequest();
        public class VerifyOTPRequest
        {
            public string PhoneNumber { get; set; } = string.Empty;
            public string UserCode { get; set; } = string.Empty;
        }
    }
}
