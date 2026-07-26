using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KasebAPI.Models.OTP
{
    public class OTPRequest
    {
        public class VerifyOTPRequest
        {
            [Key]
            public int Id { get; set; }
            public string PhoneNumber { get; set; } = string.Empty;
            public string UserCode { get; set; } = string.Empty;
            public DateTime Expiration {  get; set; }
        }
    }
}
