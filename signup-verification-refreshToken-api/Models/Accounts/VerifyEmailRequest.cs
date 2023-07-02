using System.ComponentModel.DataAnnotations;

namespace signup_verification_refreshToken_api.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
