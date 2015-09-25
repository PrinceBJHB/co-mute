using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomAuth;

namespace CoMute.Web.Models
{
    public class User : IProfileModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please provide your name")]
        [StringLength(30, ErrorMessage = "Name needs to be between {2} and {1} characters long", MinimumLength = 3)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please provide your surname")]
        [StringLength(30, ErrorMessage = "Surname needs to be between {2} and {1} characters long", MinimumLength = 3)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please provide your email address")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
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
    }
}
