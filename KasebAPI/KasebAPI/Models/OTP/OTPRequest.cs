namespace KasebAPI.Models.OTP
{
    public class OTPRequest
    {

        public class VerifyOTPRequest
        {
            public string PhoneNumber { get; set; }
            public string UserCode { get; set; }
        }
    }
}
