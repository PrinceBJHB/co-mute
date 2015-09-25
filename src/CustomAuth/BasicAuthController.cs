using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomAuth
{
    /// <summary>
    /// Web.Config - > loginUrl="~/Controller/Login"
    /// Requires AppSettings for
    /// LoginDefaultController
    /// LoginDefaultAction
    /// </summary>
    public class BasicAuthController<AppUserProfile, AppBasicUserDetails> : Controller, IBasicAuthController<AppUserProfile, AppBasicUserDetails> where AppUserProfile : class, IProfileModel, new() where AppBasicUserDetails : class, IBasicUserDetails, new()
    {
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.redirect = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppBasicUserDetails model, string ReturnUrl)
        {
            if (AuthHelper<AppUserProfile>.LoginUser(model.emailAddress, model.password, false))
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && ReturnUrl.Length > 1)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction(ConfigurationManager.AppSettings["LoginDefaultAction"], ConfigurationManager.AppSettings["LoginDefaultController"]);
                }
            }
            else
            {
                ViewBag.error = "Login Failed, Invalid username or password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                AuthHelper<AppUserProfile>.LogoutUser();
                ViewBag.error = "You have been Logged out";
            }
            return View("Login");
        }
    }
}
