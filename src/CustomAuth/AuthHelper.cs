using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Data.Linq;

namespace CustomAuth
{
    /// <summary>
    /// Authentication Helper class
    /// </summary>
    public class AuthHelper <ProfileModel> where ProfileModel : class, IProfileModel, new()
    {
        /// <summary>
        /// Will check credentials against DB and autocreate the Forms Cookie.
        /// User Profile will be accessable via AuthHelper.userProfile.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="connString"></param>
        /// <returns>success</returns>
        public static bool LoginUser(string username, string password, bool rememberMe)
        {
            ProfileModel profileModel = new ProfileModel();

            if (profileModel.Login(username, password))
            {
                profileModel.rememberMe = rememberMe;
                AuthHelper<ProfileModel>.CreateAuthenticationTicket(profileModel, new TimeSpan(365, 0, 0, 0));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Expire Forms Auth Cookie logging the user out.
        /// </summary>
        public static void LogoutUser()
        {
            if (AuthHelper<ProfileModel>.userProfile != null && !AuthHelper<ProfileModel>.userProfile.rememberMe)
            {
                ExpireAuthenticationTicket();
            }
        }

        /// <summary>
        /// Authenticated user profile.
        /// </summary>
        public static ProfileModel userProfile
        {
            get
            {
                try
                {
                    return ((Principal<ProfileModel>)HttpContext.Current.User).userProfile;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Create Auth Ticket
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="expireIn"></param>
        public static void CreateAuthenticationTicket(ProfileModel userProfile, TimeSpan expireIn)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(userProfile);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
              1, userProfile.firstName, DateTime.Now, DateTime.Now.Add(expireIn), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

        /// <summary>
        /// Expire Auth Ticket
        /// </summary>
        private static void ExpireAuthenticationTicket()
        {
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            faCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

        /// <summary>
        /// Add to Global.asax.cs
        /// protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        /// {
        ///     AuthHelper.PostAuthRequest();
        /// }
        /// </summary>
        public static void PostAuthRequest()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (authTicket.UserData == "OAuth") return;

                ProfileModel serializeModel = serializer.Deserialize<ProfileModel>(authTicket.UserData);
                Principal<ProfileModel> newUser = new Principal<ProfileModel>(authTicket.Name);

                newUser.userProfile = new ProfileModel();
                newUser.userProfile = serializeModel;

                HttpContext.Current.User = newUser;
            }
        }

        public static ProfileModel GetCookie()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (authTicket.UserData == "OAuth") return null;

                return serializer.Deserialize<ProfileModel>(authTicket.UserData);
            }

            return null;
        }
    }
}
