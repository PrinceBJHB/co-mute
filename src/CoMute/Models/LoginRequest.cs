using System.ComponentModel.DataAnnotations;
namespace CoMute.Web.Models
{
    public class LoginRequest
    {
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "This is not a valid email address.")]
        public string Email { get; set; }
		[Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
