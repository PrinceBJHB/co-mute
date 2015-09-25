
using System.ComponentModel.DataAnnotations;
namespace CoMute.Web.Models.Dto
{
    public sealed class LoginRequest : CustomAuth.IBasicUserDetails
    {
        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
