using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CustomAuth
{
    public class BasicAuth : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            //string[] users = Users.Split(',');

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            string SessionID = HttpContext.Current.Session.SessionID;
            string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            //HttpContext.Current.Session["SessionID"] = SessionID;

            return true;
        }
    }
}
