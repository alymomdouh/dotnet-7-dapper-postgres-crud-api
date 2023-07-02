using System.ComponentModel.DataAnnotations;

namespace signup_verification_refreshToken_api.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
