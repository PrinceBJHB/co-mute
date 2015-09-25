using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomAuth;

namespace CoMute.Web.Models
{
    public class UserProfile : IProfileModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please provide your name")]
        [Display(Name="Name")]
        [StringLength(30, ErrorMessage = "Name needs to be between {2} and {1} characters long", MinimumLength = 3)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please provide your surname")]
        [Display(Name = "Surname")]
        [StringLength(30, ErrorMessage = "Surname needs to be between {2} and {1} characters long", MinimumLength = 3)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please provide your email address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }

        public bool rememberMe { get; set; }
        public string userType { get; set; }
        bool IProfileModel.Login(string emailAddress, string password)
        {
            try
            {
                using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                {
                    DAL.usp_User_Login_Result result = db.usp_User_Login(emailAddress, password).FirstOrDefault();
                    if (result != null)
                    {
                        firstName = result.firstName;
                        lastName = result.lastName;
                        emailAddress = result.emailAddress;
                        phoneNumber = result.phoneNumber;

                        userType = "Admin";

                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        public bool IsInRole(string role)
        {
            try
            {
                return UserRole.Roles[role].Contains(this.userType);
            }
            catch
            {
                return false;
            }
        }

        public static implicit operator Models.UserProfile(DAL.User model)
        {
            if (model == null)
                return null;

            return new UserProfile()
            {
                UserID = model.UserID,
                firstName = model.firstName,
                lastName = model.lastName,
                emailAddress = model.emailAddress,
                phoneNumber = model.phoneNumber
            };
        }
    }
}
