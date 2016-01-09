using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
	public class User : IValidatableObject
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required.")]
		[Display(Name = "Name")]
        public string Name { get; set; }
		[Required(ErrorMessage = "Surname is required.")]
		[Display(Name = "Surname")]
        public string Surname { get; set; }
		[Display(Name = "Phone number")]
		[Phone(ErrorMessage = "This is not a valid phone number.")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "This is not a valid email address.")]
		[Display(Name = "Email address")]
        public string EmailAddress { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please confirm your password.")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext context)
		{
			if (Password != ConfirmPassword)
			{
				yield return new ValidationResult("Passwords to not match.", new string[]{"Password", "ConfirmPassword"});
			}
		}
    }
}
