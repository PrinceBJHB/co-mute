using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
	public class UpdatePasswordRequest : IValidatableObject
	{
		[Required(ErrorMessage = "Password is required.")]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required(ErrorMessage = "New password is required.")]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Please confirm your new password.")]
		[Display(Name = "Confirm new password")]
		public string ConfirmNewPassword { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext context)
		{
			if (NewPassword != ConfirmNewPassword)
			{
				yield return new ValidationResult("Passwords to not match.", new string[] { "NewPassword", "ConfirmNewPassword" });
			}
		}
	}

}
