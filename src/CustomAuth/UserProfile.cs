using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAuth
{
    /// <summary>
    /// Users Appsettings for username and password
    /// </summary>
    public class UserProfile : IProfileModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int UserID { get; set; }
        public string emailAddress { get; set; }
        public string userType { get; set; }
        public bool rememberMe { get; set; }
        bool IProfileModel.Login(string emailAddress, string password)
        {
            try
            {
                if (emailAddress.ToLower() == ConfigurationManager.AppSettings["emailAddress"] && password == ConfigurationManager.AppSettings["password"])
                {
                    emailAddress = ConfigurationManager.AppSettings["emailAddress"];
                    UserID = 1;
                    userType = "Admin";

                    return true;
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
