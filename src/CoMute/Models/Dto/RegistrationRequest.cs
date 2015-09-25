
using System.ComponentModel.DataAnnotations;
namespace CoMute.Web.Models.Dto
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Please provide your name")]
        [StringLength(30, ErrorMessage = "Name needs to be between 3 and 30 characters long", MinimumLength = 3)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please provide your surname")]
        [StringLength(30, ErrorMessage = "Surname needs to be between 3 and 30 characters long", MinimumLength = 3)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please provide your email address")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string passwordConfirm { get; set; }
    }
}
