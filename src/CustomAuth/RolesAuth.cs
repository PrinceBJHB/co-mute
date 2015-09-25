using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomAuth
{
    public static class UserRole
    {
        public const string SysAdmin_Role = "SystemAdmin";
        public const string AccAdmin_Role = "AccountAdmin";
        public const string Manager_Role = "Manager";
        public const string User_Role = "User";

        /// <summary>
        /// System Admin, For M4verick use only
        /// </summary>
        public const string SysAdmin = "SystemAdmin";

        /// <summary>
        /// Account Admin, Client Admin account
        /// </summary>
        public const string AccAdmin = "SystemAdmin,AccountAdmin";

        /// <summary>
        /// Manager, Assigned to manage specific app's or groups
        /// </summary>
        public const string Manager = "SystemAdmin,AccountAdmin,Manager";

        /// <summary>
        /// User, General User
        /// </summary>
        public const string User = "SystemAdmin,AccountAdmin,Manager,User";

        public static Dictionary<string, string> Roles = new Dictionary<string, string>()
        {
            {UserRole.SysAdmin_Role,UserRole.SysAdmin},
            {UserRole.AccAdmin_Role,UserRole.AccAdmin},
            {UserRole.Manager_Role,UserRole.Manager},
            {UserRole.User_Role,UserRole.User}
        };
    }

    public class RolesAuth : AuthorizeAttribute
    {
        /// <summary>
        /// Allow All Roles
        /// </summary>
        public RolesAuth()
        {
            Roles = "";
        }

        /// <summary>
        /// Comma seperated Roles 
        /// </summary>
        /// <param name="allowedRoles"></param>
        public RolesAuth(string allowedRoles)
        {
            Roles = allowedRoles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            string[] users = Users.Split(',');

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            string SessionID = HttpContext.Current.Session.SessionID;
            string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            //HttpContext.Current.Session["SessionID"] = SessionID;

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(Roles))
                {
                    return;
                }

                string[] requiredRoles = Roles.ToLower().Split(',');
                string userRole = ((Principal<IProfileModel>)filterContext.HttpContext.User).userProfile.userType.ToLower();

                if (!requiredRoles.Any(userRole.Contains))
                {
                    filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
                }
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = string.Format("{0}?returnUrl={1}", System.Web.Security.FormsAuthentication.LoginUrl,
                filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var redirectResult = filterContext.Result as RedirectResult;
                if (filterContext.Result is RedirectResult)
                {
                    // It was a RedirectResult => we need to calculate the url
                    var result = filterContext.Result as RedirectResult;
                    url = UrlHelper.GenerateContentUrl(result.Url, filterContext.HttpContext);
                }
                else if (filterContext.Result is RedirectToRouteResult)
                {
                    // It was a RedirectToRouteResult => we need to calculate
                    // the target url
                    var result = filterContext.Result as RedirectToRouteResult;
                    url = UrlHelper.GenerateUrl(result.RouteName, null, null, result.RouteValues, RouteTable.Routes, filterContext.RequestContext, false);
                }
                filterContext.Result = new JsonResult
                {
                    Data = new { Redirect = url },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //non-ajax request
                base.HandleUnauthorizedRequest(filterContext);
            }

        }
    }
}
